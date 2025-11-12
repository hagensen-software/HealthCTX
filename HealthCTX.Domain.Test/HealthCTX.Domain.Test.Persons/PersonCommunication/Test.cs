using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonCommunication;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
            [
            new PersonCommunication(
                new PersonCommunicationLanguage(
                    new PersonLanguageCoding(
                        new PersonLanguageCode("da"),
                        new PersonLanguageSystem(new Uri("urn:ietf:bcp:47")),
                        new PersonLanguageDisplay("Danish"))),
                new PersonPreferred(true))]);

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
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
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Person",
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

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("da", person?.Communication.First().Language.Coding.Code.Value);
        Assert.True(person?.Communication.First().Preferred.Value);
    }
}
