using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationType;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new LocationType(
                [new TypeCoding(
                    new TypeSystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-RoleCode")),
                    new TypeCode("HOSP"))])]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var code = root
            .GetProperty("type")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("HOSP", code.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "type" : [{
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/v3-RoleCode",
                        "code" : "HOSP"
                    }]
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("HOSP", location?.Types.First().Codings.First().Code?.Value);
    }
}
