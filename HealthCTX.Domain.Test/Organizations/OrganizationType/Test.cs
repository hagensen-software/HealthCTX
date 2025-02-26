using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationType;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationType(
                new OrganizationTypeCoding(
                    new OrganizationTypeCode("prov"),
                    new OrganizationTypeSystem(new Uri("http://terminology.hl7.org/CodeSystem/organization-type")))));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var types = root.GetProperty("type");
        var type = types.EnumerateArray().GetEnumerator().First();
        var codings = type.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("prov", code.GetString());
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "type" : [{
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/organization-type",
                        "code" : "prov"
                    }]
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("prov", organization?.Type.Coding.Code.Value);
    }
}
