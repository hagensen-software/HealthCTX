using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationAddress;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonGeneratesJsonString()
    {
        var organization = new Organization(
            [new OrganizationAddress(
                new OrganizationAddressText("some address"))]);

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJson(organization);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;
        var addresses = root.GetProperty("address");
        var address = addresses.EnumerateArray().GetEnumerator().First();
        var text = address.GetProperty("text");

        Assert.Equal("some address", text.GetString());
    }

    [Fact]
    public void Organization_ToFhirR5JsonGeneratesJsonStringWithoutAddress()
    {
        var organization = new Organization(
            [new OrganizationAddress(
                new OrganizationAddressText("some address"))]);

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJson(organization, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.ThrowsAny<Exception>(() => root.GetProperty("text"));
    }

    [Fact]
    public void Organization_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Organization",
                "address" : [{
                    "text" : "some address"
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("some address", organization?.Address.First().Text.Value);
    }

    [Fact]
    public void Organization_FromFhirR5JsonGeneratesRecordsWithEmptyAddresses()
    {
        var jsonString = """
            {
                "resourceType" : "Organization"
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Empty(organization?.Address!);
    }
}
