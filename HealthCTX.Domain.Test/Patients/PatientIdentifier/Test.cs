using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientIdentifier;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
                new PatientIdentifier(
                    new PatientIdentifierSystem(new Uri("http://hl7.org/fhir/sid/us-ssn")),
                    new PatientIdentifierValue("123456789")));

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var identifiers = root.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://hl7.org/fhir/sid/us-ssn", system.GetString());
        Assert.Equal("123456789", value.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Patient",
                "identifier" : [{
                    "system" : "http://hl7.org/fhir/sid/us-ssn",
                    "value" : "123456789"
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://hl7.org/fhir/sid/us-ssn", patient?.Identifier.System.Value.OriginalString);
        Assert.Equal("123456789", patient?.Identifier.Value.Value);
    }
}
