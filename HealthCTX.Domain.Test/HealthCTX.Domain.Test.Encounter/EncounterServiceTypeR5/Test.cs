using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterServiceTypeR5;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterServiceType(
                new ServiceTypeConcept(
                    [new ServiceTypeCoding(
                            new ServiceTypeSystem(new Uri("http://terminology.hl7.org/CodeSystem/service-type")),
                            new ServiceTypeCode("26"))]))]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var serviceTypeCoding = root
            .GetProperty("serviceType")
            .EnumerateArray().First()
            .GetProperty("concept")
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/service-type", serviceTypeCoding.GetProperty("system").GetString());
        Assert.Equal("26", serviceTypeCoding.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "serviceType" : [{
                    "concept" : {
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/service-type",
                            "code" : "26"
                        }]
                    }
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/service-type", encounter?.ServiceTypes.First().Concept?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("26", encounter?.ServiceTypes.First().Concept?.Codings.First().Code?.Value);
    }
}
