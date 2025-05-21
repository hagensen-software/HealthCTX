using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationOperationalStatus;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new OperationalStatus(
                new OperationalStatusSystem(new Uri("http://terminology.hl7.org/CodeSystem/v2-0116")),
                new OperationalStatusCode("O")));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJson(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("operationalStatus");

        Assert.Equal("O", status.GetProperty("code").GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "operationalStatus" : {
                    "system" : "http://terminology.hl7.org/CodeSystem/v2-0116",
                    "code" : "O"
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("O", location?.OperationalStatus?.Code?.Value);
    }
}
