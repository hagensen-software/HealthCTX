using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonGender;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
            new PersonGender("male"));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var gender = root.GetProperty("gender");

        Assert.Equal("male", gender.GetString());
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Person",
                "gender" : "male"
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("male", person?.Gender.Value);
    }
}
