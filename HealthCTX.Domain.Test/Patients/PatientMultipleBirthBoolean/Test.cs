using HealthCTX.Domain.Test.Patients.PatientDeseasedBoolean;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientMultipleBirthBoolean;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patientMultipleBirthBoolean = new PatientMultipleBirthBoolean(true);
        var patient = new Patient(patientMultipleBirthBoolean);

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        bool multipleBirth = false;
        using (var document = JsonDocument.Parse(jsonString))
        {
            JsonElement root = document.RootElement;
            multipleBirth = root.GetProperty("multipleBirthBoolean").GetBoolean();
        }

        Assert.True(multipleBirth);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "multipleBirthBoolean" : true
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.True(patient?.MultipleBirth.Value);
    }
}
