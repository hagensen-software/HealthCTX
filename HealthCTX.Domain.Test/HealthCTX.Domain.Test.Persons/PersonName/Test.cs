using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonName;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
                new PersonName(
                    new PersonFamilyName("Doe"),
                    [new PersonGivenName("John"), new PersonGivenName("D")]));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person);

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
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Person",
                "name" : [{
                    "family" : "Doe",
                    "given" : ["John", "D"]
                }]
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Doe", person?.Name.Family.Value);
        Assert.Equal("John", person?.Name.Given.First().Value);
    }
}
