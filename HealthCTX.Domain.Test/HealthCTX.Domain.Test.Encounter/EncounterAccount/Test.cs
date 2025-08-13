using HealthCTX.Domain.Test.Encounter.EncounterAppointment;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterAccount;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterAccount(
                new AccountReference("Account/123"))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var appointment = root
            .GetProperty("account")
            .EnumerateArray().First()
            .GetProperty("reference");

        Assert.Equal("Account/123", appointment.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "account" : [{
                    "reference" : "Account/123"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Account/123", encounter?.Appointments.First().Reference.Value);
    }
}
