using HealthCTX.Domain.Framework;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationTelecom;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            new OrganizationTelecom(
                new OrganizationTelecomSystem("phone"),
                new OrganizationTelecomValue("+4555555555")));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var telecoms = root.GetProperty("telecom");
        var telecom = telecoms.EnumerateArray().GetEnumerator().First();
        var system = telecom.GetProperty("system");
        var value = telecom.GetProperty("value");

        Assert.Equal("phone", system.GetString());
        Assert.Equal("+4555555555", value.GetString());
    }

    [Fact]
    public void Organization_ToFhirR5JsonGeneratesJsonStringWithoutTelecom()
    {
        var organization = new Organization(
            new OrganizationTelecom(
                new OrganizationTelecomSystem("phone"),
                new OrganizationTelecomValue("+4555555555")));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        Assert.ThrowsAny<Exception>(() => root.GetProperty("telecom"));
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "telecom" : [{
                    "system" : "phone",
                    "value" : "+4555555555"
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("phone", organization?.Telecom.System.Value);
        Assert.Equal("+4555555555", organization?.Telecom.Value.Value);
    }

    [Fact]
    public void Organization_FromFhirR5JsonFailsWithIssues()
    {
        var jsonString = """
            {
                "resourceType" : "Organization"
            }
            """;

        (_, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R5);

        Assert.NotEmpty(outcomes.Issues);
    }
}
