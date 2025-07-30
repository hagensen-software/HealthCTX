using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationTelecom;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new LocationTelecom(
                new TelecomSystem("phone"),
                new TelecomValue("+4555555555"))]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var value = root
            .GetProperty("telecom")
            .EnumerateArray().First()
            .GetProperty("value");

        Assert.Equal("+4555555555", value.GetString());
    }

    [Fact]
    public void Location_ToFhirR5JsonGeneratesJsonStringWithoutTelecom()
    {
        var location = new Location(
            [new LocationTelecom(
                new TelecomSystem("phone"),
                new TelecomValue("+4555555555"))]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.ThrowsAny<Exception>(() => root.GetProperty("telecom"));
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "telecom" : [{
                    "system" : "phone",
                    "value" : "+4555555555"
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("+4555555555", location?.Telecoms.First().Value?.Value);
    }
}
