using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientTelecom;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientTelecom(
                new PatientTelecomSystem("phone"),
                new PatientTelecomValue("+4555555555")));

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var telecoms = root.GetProperty("telecom");
        var telecom = telecoms.EnumerateArray().GetEnumerator().First();
        var system = telecom.GetProperty("system");
        var value = telecom.GetProperty("value");

        Assert.Equal("phone", system.GetString());
        Assert.Equal("+4555555555", value.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "telecom" : [{
                    "system" : "phone",
                    "value" : "+4555555555"
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("phone", patient?.Telecom.System.Value);
        Assert.Equal("+4555555555", patient?.Telecom.Value.Value);
    }
}
