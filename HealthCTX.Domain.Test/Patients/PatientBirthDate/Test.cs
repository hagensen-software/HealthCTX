using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientBirthDate;

public class Test
{
    private readonly DateOnly testDate = new(2024, 2, 14);
    private readonly string testDateString = "2024-02-14";

    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patientDeceasedDateTime = new PatientBirthDate(testDate);
        var patient = new Patient(patientDeceasedDateTime);

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJsonString(patient);

        var deceased = string.Empty;
        using (var document = JsonDocument.Parse(jsonString!))
        {
            JsonElement root = document.RootElement;
            deceased = root.GetProperty("birthDate").GetString();
        }

        Assert.Equal(testDateString, deceased);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Patient",
                "birthDate" : "{{testDateString}}"
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testDate, patient?.BirthDate.Value);
    }
}
