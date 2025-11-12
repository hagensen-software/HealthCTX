using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonTelecom;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
            new PersonTelecom(
                new PersonTelecomSystem("phone"),
                new PersonTelecomValue("+4555555555")));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var telecoms = root.GetProperty("telecom");
        var telecom = telecoms.EnumerateArray().GetEnumerator().First();
        var system = telecom.GetProperty("system");
        var value = telecom.GetProperty("value");

        Assert.Equal("phone", system.GetString());
        Assert.Equal("+4555555555", value.GetString());
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Person",
                "telecom" : [{
                    "system" : "phone",
                    "value" : "+4555555555"
                }]
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("phone", person?.Telecom.System.Value);
        Assert.Equal("+4555555555", person?.Telecom.Value.Value);
    }
}
