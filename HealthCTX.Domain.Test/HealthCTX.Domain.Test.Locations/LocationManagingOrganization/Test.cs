using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationManagingOrganization;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            new ManagingOrganization(
                new OrganizationReference("Organization/123")));

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var name = root
            .GetProperty("managingOrganization")
            .GetProperty("reference");

        Assert.Equal("Organization/123", name.GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "managingOrganization" : {
                    "reference" : "Organization/123"
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Organization/123", location?.ManagingOrganization?.Reference?.Value);
    }
}
