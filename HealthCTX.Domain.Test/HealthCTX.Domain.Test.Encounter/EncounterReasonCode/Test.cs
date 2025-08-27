using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterReasonCode;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterReasonCode(
                [new ReasonCodeCoding(
                    new ReasonCodeSystem(new Uri("http://snomed.info/sct")),
                    new ReasonCodeCode("5880005"))])]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("status");
        var reasonCode = root
            .GetProperty("reasonCode")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://snomed.info/sct", reasonCode.GetProperty("system").GetString());
        Assert.Equal("5880005", reasonCode.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "reasonCode" : [
                    {
                        "coding" : [
                            {
                                "system" : "http://snomed.info/sct",
                                "code" : "5880005"
                            }
                        ]
                    }
                ]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://snomed.info/sct", encounter?.ReasonCodes.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("5880005", encounter?.ReasonCodes.First().Codings.First().Code?.Value);
    }
}
