using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationEndpoint;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new Endpoint(
                new EndpointReference("Endpoint/123"))]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var name = root
            .GetProperty("endpoint")
            .EnumerateArray().First()
            .GetProperty("reference");

        Assert.Equal("Endpoint/123", name.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "endpoint" : [{
                    "reference" : "Endpoint/123"
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Endpoint/123", location?.Endpoints?.First().Reference?.Value);
    }
}
