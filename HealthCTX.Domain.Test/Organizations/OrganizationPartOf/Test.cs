using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationPartOf;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationPartOf(
                new OrganizationPartOfReference("Organization/123")));

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJsonString(organization);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var partOf = root.GetProperty("partOf");
        var reference = partOf.GetProperty("reference");

        Assert.Equal("Organization/123", reference.GetString());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "partOf" : {
                    "reference" : "Organization/123"
                }
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Organization/123", organization?.PartOf.Reference.Value);
    }
}
