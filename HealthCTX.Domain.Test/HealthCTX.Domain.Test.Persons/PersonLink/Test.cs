using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonLink;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var Person = new Person(
            new PersonLink(
                new PersonLinkTarget(
                    new PersonLinkOtherReference("Person/123")),
                new PersonLinkAssurance("level1")));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(Person);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var PersonLinks = root.GetProperty("link");
        var PersonLink = PersonLinks.EnumerateArray().GetEnumerator().First();
        var reference = PersonLink.GetProperty("target").GetProperty("reference");
        var type = PersonLink.GetProperty("assurance");

        Assert.Equal("Person/123", reference.GetString());
        Assert.Equal("level1", type.GetString());
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Person",
                "link" : [{
                    "target" : {
                        "reference" : "Person/123"
                    },
                    "assurance" : "level1"
                }]
            }
            """;

        (var Person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Person/123", Person?.Link.Target.Reference.Value);
        Assert.Equal("level1", Person?.Link.Assurance.Value);
    }
}
