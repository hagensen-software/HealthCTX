using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterIdentifier;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            [new Identifier(
                new IdentifierSystem(new Uri("http://example.com/encounter")),
                new IdentifierValue("12345"))],
            new Status("planned"));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var value = root
            .GetProperty("identifier")
            .EnumerateArray().First();

        Assert.Equal("http://example.com/encounter", value.GetProperty("system").GetString());
        Assert.Equal("12345", value.GetProperty("value").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "identifier": [
                    {
                        "system": "http://example.com/encounter",
                        "value": "12345"
                    }
                ],
                "status" : "planned"
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://example.com/encounter", encounter?.Identifiers.First().System.Value.ToString());
        Assert.Equal("12345", encounter?.Identifiers.First().Value.Value);
    }
}
