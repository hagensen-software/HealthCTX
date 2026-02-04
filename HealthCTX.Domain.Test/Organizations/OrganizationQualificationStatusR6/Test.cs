using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Organizations.OrganizationQualificationStatusR6;

public class Test
{
    [Fact]
    public void Organization_ToFhirJsonR6WithQualificationStatusGeneratesJsonString()
    {
        var organization = new Organization([
            new OrganizationQualification(
                new QualificationCode([new QualificationCodeCoding(new QualificationCodeCodingCode("MD"))]),
                new QualificationStatus([new QualificationStatusCoding(new QualificationStatusCodingCode("active"))]))]);

        (var jsonString, var outcomes) = OrganizationFhirJsonMapper.ToFhirJsonString(organization, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var qualification = root
            .GetProperty("qualification")
            .EnumerateArray().First();
        var codeValue = qualification
            .GetProperty("code")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");
        var statusValue = qualification
            .GetProperty("status")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("MD", codeValue.GetString());
        Assert.Equal("active", statusValue.GetString());
    }

    [Fact]
    public void Organization_ToFhirJsonR6WithoutQualificationStatusGeneratesJsonString()
    {
        var organization = new Organization([
            new OrganizationQualification(
                new QualificationCode([new QualificationCodeCoding(new QualificationCodeCodingCode("MD"))]),
                null)]);

        (var jsonString, var outcomes) = OrganizationFhirJsonMapper.ToFhirJsonString(organization, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var qualification = root
            .GetProperty("qualification")
            .EnumerateArray().First();

        Assert.False(qualification.TryGetProperty("status", out _));
    }

    [Fact]
    public void Organization_FromFhirJsonR6WithQualificationStatusGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "qualification" : [{
                    "code" : {
                        "coding" : [{
                            "code" : "MD"
                        }]
                    },
                    "status" : {
                        "coding" : [{
                            "code" : "active"
                        }]
                    }
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(organization);
        Assert.Single(organization.Qualifications);
        Assert.Equal("MD", organization.Qualifications.First().Code.Codings.First().Code.Value);
        Assert.Equal("active", organization.Qualifications.First().Status?.Codings.First().Code.Value);
    }

    [Fact]
    public void Organization_FromFhirJsonR6WithoutQualificationStatusGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Organization",
                "qualification" : [{
                    "code" : {
                        "coding" : [{
                            "code" : "MD"
                        }]
                    }
                }]
            }
            """;

        (var organization, var outcomes) = OrganizationFhirJsonMapper.ToOrganization(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(organization);
        Assert.Single(organization.Qualifications);
        Assert.Equal("MD", organization.Qualifications.First().Code.Codings.First().Code.Value);
        Assert.Null(organization.Qualifications.First().Status);
    }
}
