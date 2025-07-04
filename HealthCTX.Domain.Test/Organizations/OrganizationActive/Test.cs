using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationActive;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationActive(true));

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJsonString(organization);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var active = root.GetProperty("active");

        Assert.True(active.GetBoolean());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "active" : true
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.True(organization?.Active.Value);
    }
}
