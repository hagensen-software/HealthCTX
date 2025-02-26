using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationAlias;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationAlias("Mine"));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var names = root.GetProperty("alias");
        var name = names.EnumerateArray().First();

        Assert.Equal("Mine", name.GetString());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "alias" : ["Mine"]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Mine", organization?.Alias.Value);
    }
}
