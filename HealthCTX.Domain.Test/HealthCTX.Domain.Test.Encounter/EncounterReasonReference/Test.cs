using HealthCTX.Domain.Test.Encounter.EncounterAppointment;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterReasonReference;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterReasonReference(
                new ReasonReference("Condition/123"))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var appointment = root
            .GetProperty("reasonReference")
            .EnumerateArray().First()
            .GetProperty("reference");

        Assert.Equal("Condition/123", appointment.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "reasonReference" : [{
                    "reference" : "Condition/123"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Condition/123", encounter?.ReasonReferences.First().Reference.Value);
    }
}
