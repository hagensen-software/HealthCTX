using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationPhysicalType;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new PhysicalType(
                [new PhysicalTypeCoding(
                    new PhysicalTypeSystem(new Uri("http://terminology.hl7.org/CodeSystem/location-physical-type")),
                    new PhysicalTypeCode("si"))]));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var code = root
            .GetProperty("physicalType")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("si", code.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "physicalType" : {
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/location-physical-type",
                        "code" : "si"
                    }]
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("si", location?.PhysicalType?.Codings.First().Code?.Value);
    }
}
