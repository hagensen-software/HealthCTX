using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractionerCommunication;

public class Test
{
    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonStringR4()
    {
        var practitioner = new PractitionerR4(
            [new PractitionerCommunicationR4(
                new PractitionerCommunicationLanguageCoding(
                    new PractitionerCommunicationLanguageCode("da-DK")))]);

        (var jsonString, _) = PractitionerR4FhirJsonMapper.ToFhirJson(practitioner, Attributes.FhirVersion.R4);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var communications = root.GetProperty("communication");
        var communication = communications.EnumerateArray().GetEnumerator().First();
        var codings = communication.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();

        Assert.Equal("da-DK", coding.GetProperty("code").GetString());
    }

    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonStringR5()
    {
        var practitioner = new Practitioner(
            [new PractitionerCommunication(
                new PractitionerCommunicationLanguage(
                    new PractitionerCommunicationLanguageCoding(
                        new PractitionerCommunicationLanguageCode("da-DK"))),
                new PractitionerCommunicationPreferred(true))]);

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJson(practitioner, Attributes.FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var communications = root.GetProperty("communication");
        var communication = communications.EnumerateArray().GetEnumerator().First();
        var codings = communication.GetProperty("language").GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();

        Assert.Equal("da-DK", coding.GetProperty("code").GetString());
        Assert.True(communication.GetProperty("preferred").GetBoolean());
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecordsR4()
    {
        var jsonString = """
            {
                "resourceType":"Practitioner",
                "communication":[{
                    "coding":[{
                        "code":"da-DK"
                    }]
                }]
            }
            """;

        (var practitioner, var outcomes) = PractitionerR4FhirJsonMapper.ToPractitionerR4(jsonString, Attributes.FhirVersion.R4);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("da-DK", practitioner!.Communication[0].Coding.Code.Value);
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecordsR5()
    {
        var jsonString = """
            {
                "resourceType":"Practitioner",
                "communication":[{
                    "language":{
                        "coding":[{
                            "code":"da-DK"
                        }]
                    },
                    "preferred":true
                }]
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString, Attributes.FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("da-DK", practitioner!.Communication[0].Languages.Coding.Code.Value);
        Assert.True(practitioner.Communication[0].Preferred?.Value ?? false);
    }
}
