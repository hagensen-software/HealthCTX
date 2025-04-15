using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleLocation;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleLocation(
                new LocationReference("Location/123"))]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var locations = root.GetProperty("location");
        var location = locations.EnumerateArray().First();
        var reference = location.GetProperty("reference");

        Assert.Equal("Location/123", reference.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "location" : [{
                    "reference" : "Location/123"
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Location/123", practitionerRole?.Locations.First().Reference.Value);
    }
}
