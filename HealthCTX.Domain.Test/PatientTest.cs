using System.Text.Json;

namespace HealthCTX.Domain.Test;

public class PatientTest
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var id = new PatientId(Guid.NewGuid().ToString());

        var identifierType = new IdentifierType(
            new IdentifierCoding(new IdentifierCode("PPN")),
            new IdentifierText("Passport number"));

        var period = new IdentifierPeriod(
            new PeriodStart(DateTimeOffset.Now),
            new PeriodEnd(DateTimeOffset.Now.AddHours(1)));

        var identifiers = new List<PatientIdentifier>()
        {
            {
                new PatientIdentifier(
                    new IdentifierUse("official"),
                    identifierType,
                    new IdentifierSystem(new Uri("http://somesystem.org")),
                    new IdentifierValue("1234567890"),
                    period
                    )
            }
        };

        var codings = new List<MaritalStatusCoding>()
        {
            {
                new MaritalStatusCoding(
                    new MaritalStatusSystem(new Uri("http://somesystem.org")),
                    new MaritalStatusVersion("1.0"),
                    new MaritalStatusCode("M"),
                    new MaritalStatusDisplay("Married"),
                    new MaritalStatusUserSelected(false))
            }
        };
        var text = new MaritalStatusText("married");

        var maritalStatus = new MaritalStatus([.. codings], text);

        var patient = new Patient(id, [.. identifiers], maritalStatus);

        var json = PatientFhirJsonMapper.ToFhirJson(patient); // TODO: Datetime is not coorect

        Assert.True(IsValidJson(json));
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "id" : "75D44AFC-2F99-4816-BAEA-41E87BEDA3F0",
                "identifier" : [{
                    "use" : "usual",
                    "type" : {
                      "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/v2-0203",
                        "code" : "MR"
                      }]
                    }
                }],
                "maritalStatus" : {
                    "coding":[
                        {
                            "system":"http://somesystem.org",
                            "version":"1.0",
                            "code":"M",
                            "display":"Married",
                            "userSelected":false
                        }
                    ],
                    "text":"married"
                }
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.NotNull(patient);
    }

    [Fact]
    public void Patient_GivenInvalidJson_FromFhirReturnsErrorOutcome()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "maritalStatus" : {
                "text":"marr
            """;

        (var patient, var outcome) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Null(patient);
        Assert.Collection(outcome.Issues, issue =>
        {
            Assert.Equal("structure", issue.Code.Value);
            Assert.NotEmpty(issue.Details.Text.Value);
        });
    }

    [Fact]
    public void Patient_GivenStringWhereBooleanExpected_FromFhirReturnsErrorOutcome()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "id" : "75D44AFC-2F99-4816-BAEA-41E87BEDA3F0",
                "maritalStatus" : {
                    "coding":[
                        {
                            "system":"http://somesystem.org",
                            "version":"1.0",
                            "code":"M",
                            "display":"Married",
                            "userSelected":"false"
                        }
                    ],
                    "text":"married"
                }
            }
            """;

        (var patient, var outcome) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.NotNull(patient);
        Assert.Collection(outcome.Issues,
        issue =>
        {
            Assert.Equal("value", issue.Code.Value);
            Assert.Contains("userSelected", issue.Details.Text.Value);
        });
    }

    [Fact]
    public void Patient_GivenNumberWhereStringExpected_FromFhirReturnsErrorOutcome()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "id" : "75D44AFC-2F99-4816-BAEA-41E87BEDA3F0",
                "maritalStatus" : {
                    "coding":[
                        {
                            "system":"http://somesystem.org",
                            "version":"1.0",
                            "code":100,
                            "display":"Married",
                            "userSelected":false
                        }
                    ],
                    "text":"married"
                }
            }
            """;

        (var patient, var outcome) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.NotNull(patient);
        Assert.Collection(outcome.Issues,
        issue =>
        {
            Assert.Equal("value", issue.Code.Value);
            Assert.Contains("code", issue.Details.Text.Value);
        },
        issue =>
        {
            Assert.Equal("required", issue.Code.Value);
            Assert.Contains("code", issue.Details.Text.Value);
        });
    }

    [Fact]
    public void Patient_GivenStringWhereUriExpected_FromFhirReturnsErrorOutcome()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "id" : "75D44AFC-2F99-4816-BAEA-41E87BEDA3F0",
                "maritalStatus" : {
                    "coding":[
                        {
                            "system":"http://somesystem.org:invalid",
                            "version":"1.0",
                            "code":"M",
                            "display":"Married",
                            "userSelected":false
                        }
                    ],
                    "text":"married"
                }
            }
            """;

        (var patient, var outcome) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.NotNull(patient);
        Assert.Collection(outcome.Issues,
        issue =>
        {
            Assert.Equal("value", issue.Code.Value);
            Assert.Contains("system", issue.Details.Text.Value);
        },
        issue =>
        {
            Assert.Equal("required", issue.Code.Value);
            Assert.Contains("system", issue.Details.Text.Value);
        });
    }

    [Fact]
    public void Patient_GivenObjectWhereArrayExpected_FromFhirReturnsErrorOutcome()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "id" : "75D44AFC-2F99-4816-BAEA-41E87BEDA3F0",
                "maritalStatus" : {
                    "coding": {
                        "system":"http://somesystem.org",
                        "version":"1.0",
                        "code":"M",
                        "display":"Married",
                        "userSelected":false
                    },
                    "text":"married"
                }
            }
            """;

        (var patient, var outcome) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.NotNull(patient);
        Assert.Collection(outcome.Issues, issue =>
        {
            Assert.Equal("structure", issue.Code.Value);
            Assert.Contains("maritalStatus", issue.Details.Text.Value);
        });
    }

    private static bool IsValidJson(string jsonString)
    {
        try
        {
            JsonDocument.Parse(jsonString);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
