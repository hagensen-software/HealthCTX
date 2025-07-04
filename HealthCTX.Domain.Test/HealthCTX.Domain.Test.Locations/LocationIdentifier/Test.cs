using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationIdentifier;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new Identifier(
                new IdentifierSystem(new Uri("http://some-observation-identifier-system")),
                new IdentifierValue("12345678"))]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var identifiers = root.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://some-observation-identifier-system", system.GetString());
        Assert.Equal("12345678", value.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Location",
                "identifier" : [{
                    "system" : "http://some-observation-identifier-system",
                    "value" : "12345678"
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://some-observation-identifier-system", location?.Identifiers.First().System?.Value.OriginalString);
        Assert.Equal("12345678", location?.Identifiers.First().Value?.Value);
    }
}
