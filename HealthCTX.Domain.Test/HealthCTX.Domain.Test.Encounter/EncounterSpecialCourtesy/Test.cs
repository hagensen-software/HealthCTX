using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Test.Encounter.EncounterSpecialCourtesy;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterSpecialCourtesy;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterSpecialCourtesy(
                [new SpecialCourtesyCoding(
                    new SpecialCourtesySystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy")),
                    new SpecialCourtesyCode("EXT"))])]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var typeCoding = root
            .GetProperty("specialCourtesy")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy", typeCoding.GetProperty("system").GetString());
        Assert.Equal("EXT", typeCoding.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "specialCourtesy" : [{
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy",
                        "code" : "EXT"
                    }]
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy", encounter?.SpecialCourtesies.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("EXT", encounter?.SpecialCourtesies.First().Codings.First().Code?.Value);
    }
}
