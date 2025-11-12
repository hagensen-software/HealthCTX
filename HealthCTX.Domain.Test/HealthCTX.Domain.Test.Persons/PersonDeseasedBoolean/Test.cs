using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonDeseasedBoolean;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var personDeceasedBoolean = new PersonDeceasedBoolean(true);
        var person = new Person(personDeceasedBoolean);

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person, FhirVersion.R5);

        bool deceased = false;
        using (var document = JsonDocument.Parse(jsonString!))
        {
            JsonElement root = document.RootElement;
            deceased = root.GetProperty("deceasedBoolean").GetBoolean();
        }

        Assert.True(deceased);
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Person",
                "deceasedBoolean" : true
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.True(person?.Deceased.Value);
    }
}
