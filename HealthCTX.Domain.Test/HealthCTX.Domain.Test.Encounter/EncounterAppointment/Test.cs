using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterAppointment;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterAppointment(
                new AppointmentReference("Appointment/123"))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var appointment = root
            .GetProperty("appointment")
            .EnumerateArray().First()
            .GetProperty("reference");

        Assert.Equal("Appointment/123", appointment.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "appointment" : [{
                    "reference" : "Appointment/123"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Appointment/123", encounter?.Appointments.First().Reference.Value);
    }
}
