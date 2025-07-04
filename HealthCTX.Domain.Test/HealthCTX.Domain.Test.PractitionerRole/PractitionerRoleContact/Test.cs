using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleContact;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            new PractitionerRoleContact(
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
                        new ContactDetailOrganizationReference("Organization/123")),
                    new ContactDetailPeriod(
                        new ContactDetailPeriodStart(DateTimeOffset.Parse("2022-01-01T00:00:00Z")))));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
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
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"PractitionerRole",
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
                        "reference":"Organization/123"
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

        (var PractitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Hagensen", PractitionerRole?.Contact.Name.FamilyName.Value);
        Assert.Equal("Organization/123", PractitionerRole?.Contact.Organization?.Reference.Value);
        Assert.Equal(DateTimeOffset.Parse("2022-01-01T00:00:00Z"), PractitionerRole?.Contact.Period?.Start.Value);
    }
}
