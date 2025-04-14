using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientGeneralPractitioner;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientGeneralPractitioner(
                new GeneralPractitionerReference("Organization/123")));

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var generalPractitioners = root.GetProperty("generalPractitioner");
        var generalPractitioner = generalPractitioners.EnumerateArray().GetEnumerator().First();
        var reference = generalPractitioner.GetProperty("reference");

        Assert.Equal("Organization/123", reference.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "generalPractitioner" : [{
                    "reference" : "Organization/123"
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Organization/123", patient?.GeneralPractitioner.Reference.Value);
    }
}
