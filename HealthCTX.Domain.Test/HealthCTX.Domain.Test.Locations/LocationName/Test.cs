using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationName;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new LocationName("My Clinic"));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var name = root.GetProperty("name");

        Assert.Equal("My Clinic", name.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "name" : "My Clinic"
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("My Clinic", location?.Name?.Value);
    }
}
