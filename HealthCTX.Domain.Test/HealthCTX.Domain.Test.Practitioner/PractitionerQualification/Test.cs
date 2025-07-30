using HealthCTX.Domain.Identifiers;
using System.Globalization;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerQualification;

public class Test
{
    private readonly DateTimeOffset startDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));

    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitioner = new Practitioner(
            [new PractitionerQualification(
                [new PractitionerQualificationIdentifier(
                    new PractitionerIdentifierSystem(
                        new Uri("http://some-practitioner-qualification.org")),
                    new PractitionerIdentifierValue("1234567890"))],
                new PractitionerQualificationCode(
                    new PractitionerQualificationText("some qualification")),
                new PractitionerQualificationPeriod(
                    new PractitionerQualificationStart(startDate)),
                new PractitionerQualificationIssuer(
                    new PractitionerQualificationIssuerReference("Organization/123")))]);


        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJsonString(practitioner);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var qualifications = root.GetProperty("qualification");
        var qualification = qualifications.EnumerateArray().GetEnumerator().First();
        var identifiers = qualification.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();

        Assert.Equal("http://some-practitioner-qualification.org", identifier.GetProperty("system").GetString()!);
        Assert.Equal("1234567890", identifier.GetProperty("value").GetString()!);
        Assert.Equal("some qualification", qualification.GetProperty("code").GetProperty("text").GetString()!);
        Assert.Equal("2024-02-14T13:42:00+01:00", qualification.GetProperty("period").GetProperty("start").GetString()!);
        Assert.Equal("Organization/123", qualification.GetProperty("issuer").GetProperty("reference").GetString()!);
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"Practitioner",
                "qualification":[{
                    "code":{
                        "text":"some qualification"
                    },
                    "period":{
                        "start":"2024-02-14T13:42:00+01:00"
                    },
                    "issuer":{
                        "reference":"Organization/123"
                    },
                    "identifier":[{
                        "system":"http://some-practitioner-qualification.org",
                        "value":"1234567890"
                    }]
                }]
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);

        var qualification = Assert.Single(practitioner!.Qualifications);
        var identifier = Assert.Single(qualification.Identifiers);

        Assert.Equal("http://some-practitioner-qualification.org", identifier.System.Value.OriginalString);
        Assert.Equal("1234567890", identifier.Value.Value);
        Assert.Equal("some qualification", qualification.Code.Text.Value);
        Assert.Equal(startDate, qualification.Period!.Start.Value);
        Assert.Equal("Organization/123", qualification.Issuer!.Reference.Value);

    }
}
