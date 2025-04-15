using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleOrganization;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            new PractitionerRoleOrganization(
                new OrganizationReference("Organization/123")));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var practitioner = root.GetProperty("organization");
        var reference = practitioner.GetProperty("reference");

        Assert.Equal("Organization/123", reference.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "organization" : {
                    "reference" : "Organization/123"
                }
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Organization/123", practitionerRole?.Organization.Reference.Value);
    }
}
