using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Test.Locations.LocationType;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationCharacteristic;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new LocationCharacteristic(
                [new CharacteristicCoding(
                    new CharacteristicSystem(new Uri("http://hl7.org/fhir/location-characteristic")),
                    new CharacteristicCode("wheelchair"))])]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var code = root
            .GetProperty("characteristic")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("wheelchair", code.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "characteristic" : [{
                    "coding" : [{
                        "system" : "http://hl7.org/fhir/location-characteristic",
                        "code" : "wheelchair"
                    }]
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("wheelchair", location?.Types.First().Codings.First().Code?.Value);
    }
}
