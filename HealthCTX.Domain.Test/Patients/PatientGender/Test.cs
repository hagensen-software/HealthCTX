using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientGender;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientGender("male"));

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var gender = root.GetProperty("gender");

        Assert.Equal("male", gender.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "gender" : "male"
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("male", patient?.Gender.Value);
    }
}
