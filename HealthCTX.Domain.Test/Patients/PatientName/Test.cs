using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientName;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
                new PatientName(
                    new PatientFamilyName("Doe"),
                    [new PatientGivenName("John"), new PatientGivenName("D")]));

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJsonString(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var names = root.GetProperty("name");
        var name = names.EnumerateArray().GetEnumerator().First();
        var family = name.GetProperty("family");
        var givens = name.GetProperty("given");
        var given = givens.EnumerateArray().GetEnumerator().First();

        Assert.Equal("Doe", family.GetString());
        Assert.Equal("John", given.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Patient",
                "name" : [{
                    "family" : "Doe",
                    "given" : ["John", "D"]
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Doe", patient?.Name.Family.Value);
        Assert.Equal("John", patient?.Name.Given.First().Value);
    }
}
