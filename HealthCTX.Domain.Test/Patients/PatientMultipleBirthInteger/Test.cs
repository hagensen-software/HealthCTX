using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientMultipleBirthInteger;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patientMultipleBirthInteger = new PatientMultipleBirthInteger(1);
        var patient = new Patient(patientMultipleBirthInteger);

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJsonString(patient);

        int multipleBirthInteger = 0;
        using (var document = JsonDocument.Parse(jsonString!))
        {
            JsonElement root = document.RootElement;
            multipleBirthInteger = root.GetProperty("multipleBirthInteger").GetInt32();
        }

        Assert.Equal(1, multipleBirthInteger);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "multipleBirthInteger" : 1
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(1, patient?.MultipleBirthNumber.Value);
    }
}
