using HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleOrganization;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleEndpoint;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            new PractitionerRoleEndpoint(
                new EndpointReference("Endpoint/123")));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var endpoints = root.GetProperty("endpoint");
        var endpoint = endpoints.EnumerateArray().First();
        var reference = endpoint.GetProperty("reference");

        Assert.Equal("Endpoint/123", reference.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "endpoint" : [{
                    "reference" : "Endpoint/123"
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Endpoint/123", practitionerRole?.Endpoint.Reference.Value);
    }
}
