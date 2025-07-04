using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientManagingOrganization;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientManagingOrganization(
                new PatientManagingOrganizationReference("Organization/123")));

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJsonString(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var managingOrganization = root.GetProperty("managingOrganization");
        var reference = managingOrganization.GetProperty("reference");

        Assert.Equal("Organization/123", reference.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "managingOrganization" : {
                    "reference" : "Organization/123"
                }
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Organization/123", patient?.ManagingOrganization.Reference.Value);
    }
}
