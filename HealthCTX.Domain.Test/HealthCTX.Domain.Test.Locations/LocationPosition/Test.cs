using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationPosition;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new LocationPosition(
                new Longitude(10.203921),
                new Latutude(56.162939),
                new Altitude(23.0)));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var position = root
            .GetProperty("position");

        Assert.Equal(10.203921, position.GetProperty("longitude").GetDouble());
        Assert.Equal(56.162939, position.GetProperty("latitude").GetDouble());
        Assert.Equal(23.0, position.GetProperty("altitude").GetDouble());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "position" : {
                    "longitude" : 10.203921,
                    "latitude" : 56.162939,
                    "altitude" : 23.0
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(10.203921, location?.Position?.Longitude?.Value);
        Assert.Equal(56.162939, location?.Position?.Latitude?.Value);
        Assert.Equal(23.0, location?.Position?.Altitude?.Value);
    }
}
