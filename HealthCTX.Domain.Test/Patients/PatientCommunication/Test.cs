using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientCommunication;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            [
            new PatientCommunication(
                new PatientCommunicationLanguage(
                    new PatientLanguageCoding(
                        new PatientLanguageCode("da"),
                        new PatientLanguageSystem(new Uri("urn:ietf:bcp:47")),
                        new PatientLanguageDisplay("Danish"))),
                new PatientPreferred(true))]);

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;
        var communications = root.GetProperty("communication");
        var communication = communications.EnumerateArray().GetEnumerator().First();
        var language = communication.GetProperty("language");
        var codings = language.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");
        var preferred = communication.GetProperty("preferred");

        Assert.Equal(JsonValueKind.Object, communication.ValueKind);
        Assert.Equal(JsonValueKind.Object, language.ValueKind);
        Assert.Equal(JsonValueKind.Object, coding.ValueKind);
        Assert.Equal("da", code.GetString());
        Assert.True(preferred.GetBoolean());
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Patient",
                "communication" : [{
                    "language" : {
                        "coding" : [{
                            "system" : "urn:ietf:bcp:47",
                              "code" : "da",
                              "display" : "Danish"
                        }]
                    },
                    "preferred" : true
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("da", patient?.Communication.First().Language.Coding.Code.Value);
        Assert.True(patient?.Communication.First().Preferred.Value);
    }
}
