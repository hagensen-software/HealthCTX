using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationAddress;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new LocationAddress(
                new AddressText("some address")));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var text = root
            .GetProperty("address")
            .GetProperty("text");

        Assert.Equal("some address", text.GetString());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Location",
                "address" : {
                    "text" : "some address"
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("some address", location?.Address?.Text.Value);
    }
}
