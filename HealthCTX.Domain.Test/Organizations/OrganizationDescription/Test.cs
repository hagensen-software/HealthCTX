using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationDescription;

public class Test
{
    [Fact]
    public void Organization_ToFhirR5JsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationDescription("some description"));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var description = root.GetProperty("description");

        Assert.Equal("some description", description.GetString());
    }

    [Fact]
    public void Organization_ToFhirR4JsonGeneratesJsonStringWithoutDescription()
    {
        var organization = new Organization(
            new OrganizationDescription("some description"));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization, FhirVersion.R4);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        Assert.ThrowsAny<Exception>(() => root.GetProperty("description"));
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "description" : "some description"
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("some description", organization?.Description.Value);
    }

    [Fact]
    public void Organization_FromFhirR4JsonFailsWithIssues()
    {
        var jsonString = """
            {
                "resourceType" : "Organization"
            }
            """;

        (_, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R4);

        Assert.NotEmpty(outcomes.Issues);
    }
}
