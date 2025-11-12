using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonDeceasedDateTime;

public class Test
{
    private readonly DateTimeOffset testDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string testDateString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var personDeceasedDateTime = new PersonDeceasedDateTime(testDate);
        var person = new Person(personDeceasedDateTime);

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person, FhirVersion.R5);

        var deceased = string.Empty;
        using (var document = JsonDocument.Parse(jsonString!))
        {
            JsonElement root = document.RootElement;
            deceased = root.GetProperty("deceasedDateTime").GetString();
        }

        Assert.Equal(testDateString, deceased);
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Person",
                "deceasedDateTime" : "{{testDateString}}"
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testDate, person?.Deceased.Value);
    }
}
