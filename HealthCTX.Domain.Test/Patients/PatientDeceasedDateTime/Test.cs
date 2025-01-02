using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientDeceasedDateTime;

public class Test
{
    private readonly DateTimeOffset testDate = new DateTimeOffset(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string testDateString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patientDeceasedDateTime = new PatientDeceasedDateTime(testDate);
        var patient = new Patient(patientDeceasedDateTime);

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        var deceased = string.Empty;
        using (var document = JsonDocument.Parse(jsonString))
        {
            JsonElement root = document.RootElement;
            deceased = root.GetProperty("deceasedDateTime").GetString();
        }

        Assert.Equal(testDateString, deceased);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Patient",
                "deceasedDateTime" : "{{testDateString}}"
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testDate, patient?.Deceased.Value);
    }
}
