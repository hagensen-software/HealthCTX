using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonBirthDate;

public class Test
{
    private readonly DateOnly testDate = new(2024, 2, 14);
    private readonly string testDateString = "2024-02-14";

    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var personDeceasedDateTime = new PersonBirthDate(testDate);
        var person = new Person(personDeceasedDateTime);

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person);

        var deceased = string.Empty;
        using (var document = JsonDocument.Parse(jsonString!))
        {
            JsonElement root = document.RootElement;
            deceased = root.GetProperty("birthDate").GetString();
        }

        Assert.Equal(testDateString, deceased);
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Person",
                "birthDate" : "{{testDateString}}"
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testDate, person?.BirthDate.Value);
    }
}
