using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;
using System.Collections.Immutable;
using System.Text.Json;

namespace HealthCTX.Domain.Test;

public class PatientTest
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
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

        var maritalStatus = new MaritalStatus(codings.ToImmutableList(), text);

        var patient = new Patient(maritalStatus);

        var json = PatientFhirJsonMapper.ToFhirJson(patient);

        Assert.True(IsValidJson(json));
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
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

    private bool IsValidJson(string jsonString)
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
