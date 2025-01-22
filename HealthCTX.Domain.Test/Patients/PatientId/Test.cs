using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientId;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientId("123"));

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var id = root.GetProperty("id");

        Assert.Equal("123", id.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "id" : "123"
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("123", patient?.Id.Value);
    }
}
