using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonIdentifier;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
                new PersonIdentifier(
                    new PersonIdentifierSystem(new Uri("http://hl7.org/fhir/sid/us-ssn")),
                    new PersonIdentifierValue("123456789")));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var identifiers = root.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://hl7.org/fhir/sid/us-ssn", system.GetString());
        Assert.Equal("123456789", value.GetString());
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Person",
                "identifier" : [{
                    "system" : "http://hl7.org/fhir/sid/us-ssn",
                    "value" : "123456789"
                }]
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://hl7.org/fhir/sid/us-ssn", person?.Identifier.System.Value.OriginalString);
        Assert.Equal("123456789", person?.Identifier.Value.Value);
    }
}
