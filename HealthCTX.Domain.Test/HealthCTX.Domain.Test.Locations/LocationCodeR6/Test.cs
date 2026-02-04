using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationCodeR6;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonR6WithCodeGeneratesJsonString()
    {
        var location = new Location(
            new LocationCode([new LocationCodeCoding(new LocationCodeCodingCode("HOSP"))]));

        (var jsonString, var outcomes) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var codeValue = root
            .GetProperty("code")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("HOSP", codeValue.GetString());
    }

    [Fact]
    public void Location_ToFhirJsonR6WithoutCodeGeneratesJsonString()
    {
        var location = new Location(null);

        (var jsonString, var outcomes) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.False(root.TryGetProperty("code", out _));
    }

    [Fact]
    public void Location_FromFhirJsonR6WithCodeGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "code" : {
                    "coding" : [{
                        "code" : "HOSP"
                    }]
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(location);
        Assert.Equal("HOSP", location.Code?.Codings.First().Code.Value);
    }

    [Fact]
    public void Location_FromFhirJsonR6WithoutCodeGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location"
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(location);
        Assert.Null(location.Code);
    }
}
