using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationIdentifier;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
                new OrganizationIdentifier(
                    new OrganizationIdentifierSystem(new Uri("http://cvr.dk")),
                    new OrganizationIdentifierValue("12345678")));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var identifiers = root.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://cvr.dk", system.GetString());
        Assert.Equal("12345678", value.GetString());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Organization",
                "identifier" : [{
                    "system" : "http://cvr.dk",
                    "value" : "12345678"
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://cvr.dk", organization?.Identifier.System.Value.OriginalString);
        Assert.Equal("12345678", organization?.Identifier.Value.Value);
    }
}
