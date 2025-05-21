using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationAlias;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new LocationAlias("My Clinic")]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var alias = root.GetProperty("alias").EnumerateArray().First();

        Assert.Equal("My Clinic", alias.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "alias" : [
                  "My Clinic"
                ]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("My Clinic", location?.Aliasses.First().Value);
    }
}
