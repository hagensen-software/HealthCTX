using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationQualification;

public class Test
{
    [Fact]
    public void Organization_ToFhirR5JsonGeneratesJsonString()
    {
        var organization = new Organization(
            [new OrganizationQualification(
                [new OrganizationQualificationIdentifier(
                    new OrganizationQualificationIdentifierSystem(new Uri("http://qualifications.org")),
                    new OrganizationQualificationIdentifierValue("q123"))],
                new OrganizationQualificationCode(
                    new OrganizationQualificationCodeText("some qualification")),
                new OrganizationQualificationPeriod(
                    new OrganizationQualificationPeriodStart(DateTimeOffset.Parse("2022-01-01T00:00:00+00:00"))),
                new OrganizationQualificationIssuer(
                    new OrganizationQualificationIssuerReference("Organization/123")))]);

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJsonString(organization, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;
        var qualifications = root.GetProperty("qualification");
        var qualification = qualifications.EnumerateArray().GetEnumerator().First();
        var identifiers = qualification.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var identifierValue = identifier.GetProperty("value");
        var code = qualification.GetProperty("code");
        var codeText = code.GetProperty("text");
        var period = qualification.GetProperty("period");
        var periodStart = period.GetProperty("start");
        var issuer = qualification.GetProperty("issuer");
        var issuerReference = issuer.GetProperty("reference");

        Assert.Equal("q123", identifierValue.GetString());
        Assert.Equal("some qualification", codeText.GetString());
        Assert.Equal("2022-01-01T00:00:00+00:00", periodStart.GetString());
        Assert.Equal("Organization/123", issuerReference.GetString());
    }

    [Fact]
    public void Organization_ToFhirR4JsonGeneratesJsonStringWithoutQualification()
    {
        var organization = new Organization(
            [new OrganizationQualification(
                [new OrganizationQualificationIdentifier(
                    new OrganizationQualificationIdentifierSystem(new Uri("http://qualifications.org")),
                    new OrganizationQualificationIdentifierValue("q123"))],
                new OrganizationQualificationCode(
                    new OrganizationQualificationCodeText("some qualification")),
                new OrganizationQualificationPeriod(
                    new OrganizationQualificationPeriodStart(DateTimeOffset.Parse("2022-01-01T00:00:00Z"))),
                new OrganizationQualificationIssuer(
                    new OrganizationQualificationIssuerReference("Organization/123")))]);

        (var jsonString, _) = OrganizationFhirJsonMapper.ToFhirJsonString(organization);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.ThrowsAny<Exception>(() => root.GetProperty("qualification"));
    }

    [Fact]
    public void Organization_FromFhirR5JsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"Organization",
                "qualification":[{
                    "code":{
                        "text":"some qualification"
                    },
                    "period":{
                        "start":"2022-01-01T00:00:00+00:00"
                    },
                    "issuer":{
                        "reference":"Organization/123"
                    },
                    "identifier":[{
                        "system":"http://qualifications.org",
                        "value":"q123"
                    }]
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("q123", organization?.Qualifications.First().Identifier.First().Value.Value);
        Assert.Equal(DateTimeOffset.Parse("2022-01-01T00:00:00+00:00"), organization?.Qualifications.First().Period.Start.Value);
        Assert.Equal("some qualification", organization?.Qualifications.First().Code.Text.Value);
        Assert.Equal("Organization/123", organization?.Qualifications.First().Issuer.Reference.Value);
    }

    [Fact]
    public void Organization_FromFhirR4JsonGeneratesRecordsWithEmptyQualification()
    {
        var jsonString = """
            {
                "resourceType" : "Organization"
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Empty(organization?.Qualifications!);
    }
}
