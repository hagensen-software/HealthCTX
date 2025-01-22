using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientActive;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientActive(true));

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var active = root.GetProperty("active");

        Assert.True(active.GetBoolean());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "active" : true
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.True(patient?.Active.Value);
    }
}
