using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterReason;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterReason(
                [new ReasonUse(
                    [new UseCoding(
                        new UseSystem(new Uri("http://hl7.org/fhir/encounter-reason-use")),
                        new UseCode("HC"))])],
                [new ReasonValue(
                    new ReasonReference(
                        new ReasonReferenceReference("Condition/123")))])]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("status");
        var reasonUse = root
            .GetProperty("reason")
            .EnumerateArray().First()
            .GetProperty("use")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();
        var reasonValue = root
            .GetProperty("reason")
            .EnumerateArray().First()
            .GetProperty("value")
            .EnumerateArray().First()
            .GetProperty("reference")
            .GetProperty("reference");

        Assert.Equal("planned", status.GetString());
        Assert.Equal("http://hl7.org/fhir/encounter-reason-use", reasonUse.GetProperty("system").GetString());
        Assert.Equal("HC", reasonUse.GetProperty("code").GetString());
        Assert.Equal("Condition/123", reasonValue.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "reason" : [{
                    "use" : [{
                        "coding" : [{
                            "system" : "http://hl7.org/fhir/encounter-reason-use",
                            "code" : "HC"
                        }]
                    }],
                    "value" : [{
                        "reference" : {
                            "reference" : "Condition/123"
                        }
                    }]
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://hl7.org/fhir/encounter-reason-use", encounter?.Reasons.First().Uses.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("HC", encounter?.Reasons.First().Uses.First().Codings.First().Code?.Value);
        Assert.Equal("Condition/123", encounter?.Reasons.First().Values.First().Reference.Value.Value);
    }
}
