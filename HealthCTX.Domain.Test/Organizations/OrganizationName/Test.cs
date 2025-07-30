using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationName;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationName("My Clinic"));

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJsonString(organization);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var name = root.GetProperty("name");

        Assert.Equal("My Clinic", name.GetString());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "name" : "My Clinic"
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("My Clinic", organization?.Name.Value);
    }
}
