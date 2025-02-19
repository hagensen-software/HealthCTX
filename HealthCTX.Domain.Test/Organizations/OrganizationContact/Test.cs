using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationContact;

public class Test
{
    [Fact]
    public void Organization_ToR4FhirJsonGeneratesJsonStringWithoutOrganizationAndPeriod()
    {
        var organization = new Organization(
            new OrganizationContact(
                new ContactDetailPurpose(
                    new ContactDetailPurposeCoding(
                        new ContactDetailPurposeSystem(new Uri("http://terminology.hl7.org/CodeSystem/contactentity-type")),
                        new ContactDetailPurposeCode("admin"))),
                    new ContactDetailName(
                        new ContactDetailFamilyName("Hagensen")),
                    new ContactDetailTelecom(
                        new ContactDetailTeleconSystem("phone"),
                        new ContactDetailTeleconValue("555-555-5555")),
                    new ContactDetailAddress(
                        new ContactDetailAddressText("123 Main St.")),
                    new ContactDetailOrganization(
                        new ContactDetailOrganizationReference("Organization/1")),
                    new ContactDetailPeriod(
                        new ContactDetailPeriodStart(DateTimeOffset.Parse("2022-01-01T00:00:00Z")))));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var contacts = root.GetProperty("contact");
        var contact = contacts.EnumerateArray().GetEnumerator().First();
        var names = contact.GetProperty("name");
        var name = names.EnumerateArray().GetEnumerator().First();
        var family = name.GetProperty("family");

        Assert.Equal("Hagensen", family.GetString());
        Assert.False(contact.TryGetProperty("organization", out _));
        Assert.False(contact.TryGetProperty("period", out _));
    }

    [Fact]
    public void Organization_ToR5FhirJsonGeneratesJsonStringWithOrganizationAndPeriod()
    {
        var organization = new Organization(
            new OrganizationContact(
                new ContactDetailPurpose(
                    new ContactDetailPurposeCoding(
                        new ContactDetailPurposeSystem(new Uri("http://terminology.hl7.org/CodeSystem/contactentity-type")),
                        new ContactDetailPurposeCode("admin"))),
                    new ContactDetailName(
                        new ContactDetailFamilyName("Hagensen")),
                    new ContactDetailTelecom(
                        new ContactDetailTeleconSystem("phone"),
                        new ContactDetailTeleconValue("555-555-5555")),
                    new ContactDetailAddress(
                        new ContactDetailAddressText("123 Main St.")),
                    new ContactDetailOrganization(
                        new ContactDetailOrganizationReference("Organization/1")),
                    new ContactDetailPeriod(
                        new ContactDetailPeriodStart(DateTimeOffset.Parse("2022-01-01T00:00:00Z")))));

        var jsonString = OrganizationFhirJsonMapper.ToFhirJson(organization, Framework.FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        var contacts = root.GetProperty("contact");
        var contact = contacts.EnumerateArray().GetEnumerator().First();
        var names = contact.GetProperty("name");
        var name = names.EnumerateArray().GetEnumerator().First();
        var family = name.GetProperty("family");

        Assert.Equal("Hagensen", family.GetString());
        Assert.True(contact.TryGetProperty("organization", out _));
        Assert.True(contact.TryGetProperty("period", out _));
    }

    [Fact]
    public void Organization_FromR4FhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"Organization",
                "contact":[{
                    "purpose":{
                        "coding":[{
                            "system":"http://terminology.hl7.org/CodeSystem/contactentity-type",
                            "code":"admin"
                        }]
                    },
                    "address":{
                        "text":"123 Main St."
                    },
                    "name":[{
                        "family":"Hagensen"
                    }],
                    "telecom":[{
                        "system":"phone",
                        "value":"555-555-5555"
                    }]
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Hagensen", organization?.Contact.Name.FamilyName.Value);
    }

    [Fact]
    public void Organization_FromR5FhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"Organization",
                "contact":[{
                    "purpose":{
                        "coding":[{
                            "system":"http://terminology.hl7.org/CodeSystem/contactentity-type",
                            "code":"admin"
                        }]
                    },
                    "address":{
                        "text":"123 Main St."
                    },
                    "organization":{
                        "reference":"Organization/1"
                    },
                    "period":{
                        "start":"2022-01-01T00:00:00\u002B00:00"
                    },
                    "name":[{
                        "family":"Hagensen"
                    }],
                    "telecom":[{
                        "system":"phone",
                        "value":"555-555-5555"
                    }]
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, Framework.FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Hagensen", organization?.Contact.Name.FamilyName.Value);
        Assert.Equal("Organization/1", organization?.Contact.Organization?.Reference.Value);
        Assert.Equal(DateTimeOffset.Parse("2022-01-01T00:00:00Z"), organization?.Contact.Period?.Start.Value);
    }

    [Fact]
    public void Organization_FromR5FhirAsR4JsonGeneratesRecordsWithoutOrganizationAndPeriod()
    {
        var jsonString = """
            {
                "resourceType":"Organization",
                "contact":[{
                    "purpose":{
                        "coding":[{
                            "system":"http://terminology.hl7.org/CodeSystem/contactentity-type",
                            "code":"admin"
                        }]
                    },
                    "address":{
                        "text":"123 Main St."
                    },
                    "organization":{
                        "reference":"Organization/1"
                    },
                    "period":{
                        "start":"2022-01-01T00:00:00\u002B00:00"
                    },
                    "name":[{
                        "family":"Hagensen"
                    }],
                    "telecom":[{
                        "system":"phone",
                        "value":"555-555-5555"
                    }]
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString);

        Assert.Equal(2, outcomes.Issues.Count);
        Assert.Equal("Hagensen", organization?.Contact.Name.FamilyName.Value);
        Assert.Null(organization?.Contact.Organization);
        Assert.Null(organization?.Contact.Period);
    }
}
