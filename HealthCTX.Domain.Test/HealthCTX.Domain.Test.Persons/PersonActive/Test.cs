using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonActive;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
            new PersonActive(true));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var active = root.GetProperty("active");

        Assert.True(active.GetBoolean());
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Person",
                "active" : true
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.True(person?.Active.Value);
    }
}
