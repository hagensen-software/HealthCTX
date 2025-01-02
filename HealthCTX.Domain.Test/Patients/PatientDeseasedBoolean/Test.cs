using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientDeseasedBoolean;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patientDeceasedBoolean = new PatientDeceasedBoolean(true);
        var patient = new Patient(patientDeceasedBoolean);

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        bool deceased = false;
        using (var document = JsonDocument.Parse(jsonString))
        {
            JsonElement root = document.RootElement;
            deceased = root.GetProperty("deceasedBoolean").GetBoolean();
        }

        Assert.True(deceased);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "deceasedBoolean" : true
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.True(patient?.Deceased.Value);
    }
}
