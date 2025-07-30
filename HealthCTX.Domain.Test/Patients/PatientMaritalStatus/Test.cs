using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientMaritalStatus;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new PatientMaritalStatus(
                new PatientMaritalStatusCoding(
                    new PatientMaritalStatusCode("M"),
                    new PatientMaritalStatusSystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus")),
                    new PatientMaritalStatusDisplay("Married")),
                new PatientMaritalStatusText("Married")));

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJsonString(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var maritalStatus = root.GetProperty("maritalStatus");
        var codings = maritalStatus.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");
        var text = maritalStatus.GetProperty("text");

        Assert.Equal("M", code.GetString());
        Assert.Equal("Married", text.GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "maritalStatus" : {
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus",
                        "code" : "M",
                        "display" : "Married"
                    }],
                    "text" : "Married"
                }
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("M", patient?.MaritalStatus.Coding.Code.Value);
        Assert.Equal("Married", patient?.MaritalStatus.Coding.Display.Value);
    }
}
