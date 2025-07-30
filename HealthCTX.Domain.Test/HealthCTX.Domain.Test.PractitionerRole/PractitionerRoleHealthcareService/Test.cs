using HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleLocation;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleHealthcareService;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleHealthcareService(
                new HealthcareServiceReference("HealthcareService/123"))]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var locations = root.GetProperty("healthcareService");
        var location = locations.EnumerateArray().First();
        var reference = location.GetProperty("reference");

        Assert.Equal("HealthcareService/123", reference.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "healthcareService" : [{
                    "reference" : "HealthcareService/123"
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("HealthcareService/123", practitionerRole?.HealthcareServices.First().Reference.Value);
    }
}
