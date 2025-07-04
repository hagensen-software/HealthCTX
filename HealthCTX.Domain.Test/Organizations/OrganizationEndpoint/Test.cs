using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationEndpoint;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationEndpoint(
                new OrganizationEndpointReference("Endpoint/123")));

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJsonString(organization);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var endpoints = root.GetProperty("endpoint");
        var endpoint = endpoints.EnumerateArray().First();
        var reference = endpoint.GetProperty("reference");

        Assert.Equal("Endpoint/123", reference.GetString());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "endpoint" : [{
                    "reference" : "Endpoint/123"
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Endpoint/123", organization?.Endpoint.Reference.Value);
    }
}
