using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationForm;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new LocationForm(
                [new FormCoding(
                    new FormSystem(new Uri("http://terminology.hl7.org/CodeSystem/location-physical-type")),
                    new FormCode("si"))]));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var code = root
            .GetProperty("form")
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
                "form" : {
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/location-physical-type",
                        "code" : "si"
                    }]
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("si", location?.Form?.Codings.First().Code?.Value);
    }
}
