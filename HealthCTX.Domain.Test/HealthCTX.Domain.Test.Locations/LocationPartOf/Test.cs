using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationPartOf;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new LocationPartOf(
                new LocationReference("Location/123")));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var name = root
            .GetProperty("partOf")
            .GetProperty("reference");

        Assert.Equal("Location/123", name.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "partOf" : {
                    "reference" : "Location/123"
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Location/123", location?.PartOf?.Reference?.Value);
    }
}
