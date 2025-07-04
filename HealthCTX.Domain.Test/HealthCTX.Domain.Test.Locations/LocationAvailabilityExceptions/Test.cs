using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationAvailabilityExceptions;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new LocationAvailabilityExceptions("Not always available"));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var description = root.GetProperty("availabilityExceptions");

        Assert.Equal("Not always available", description.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "availabilityExceptions" : "Not always available"
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Not always available", location?.AvailabilityExceptions?.Value);
    }
}
