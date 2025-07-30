using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientLink;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientLink(
                new PatientLinkOther(
                    new PatientLinkOtherReference("Patient/123")),
                new PatientLinkType("seealso")));

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJsonString(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var patientLinks = root.GetProperty("link");
        var patientLink = patientLinks.EnumerateArray().GetEnumerator().First();
        var reference = patientLink.GetProperty("other").GetProperty("reference");
        var type = patientLink.GetProperty("type");

        Assert.Equal("Patient/123", reference.GetString());
        Assert.Equal("seealso", type.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "link" : [{
                    "other" : {
                        "reference" : "Patient/123"
                    },
                    "type" : "seealso"
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Patient/123", patient?.Link.Other.Reference.Value);
        Assert.Equal("seealso", patient?.Link.Type.Value);
    }
}
