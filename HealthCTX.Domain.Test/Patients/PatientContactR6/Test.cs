using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientContactR6;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonR6WithContactRoleGeneratesJsonString()
    {
        var patient = new Patient([
            new PatientContact(
                [new ContactRole([new ContactRoleCoding(new ContactRoleCodingCode("emergency"))])],
                [],
                [])]);

        (var jsonString, var outcomes) = PatientFhirJsonMapper.ToFhirJsonString(patient, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var contact = root
            .GetProperty("contact")
            .EnumerateArray().First();
        var roleCode = contact
            .GetProperty("role")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("emergency", roleCode.GetString());
    }

    [Fact]
    public void Patient_ToFhirJsonR6WithAdditionalNameGeneratesJsonString()
    {
        var patient = new Patient([
            new PatientContact(
                [],
                [new AdditionalName(
                    new AdditionalNameFamily("Smith"),
                    [new AdditionalNameGiven("Jane")])],
                [])]);

        (var jsonString, var outcomes) = PatientFhirJsonMapper.ToFhirJsonString(patient, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var contact = root
            .GetProperty("contact")
            .EnumerateArray().First();
        var additionalName = contact
            .GetProperty("additionalName")
            .EnumerateArray().First();

        Assert.Equal("Smith", additionalName.GetProperty("family").GetString());
        Assert.Equal("Jane", additionalName.GetProperty("given").EnumerateArray().First().GetString());
    }

    [Fact]
    public void Patient_ToFhirJsonR6WithAdditionalAddressGeneratesJsonString()
    {
        var patient = new Patient([
            new PatientContact(
                [],
                [],
                [new AdditionalAddress(
                    new AdditionalAddressCity("Copenhagen"),
                    new AdditionalAddressCountry("Denmark"))])]);

        (var jsonString, var outcomes) = PatientFhirJsonMapper.ToFhirJsonString(patient, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var contact = root
            .GetProperty("contact")
            .EnumerateArray().First();
        var additionalAddress = contact
            .GetProperty("additionalAddress")
            .EnumerateArray().First();

        Assert.Equal("Copenhagen", additionalAddress.GetProperty("city").GetString());
        Assert.Equal("Denmark", additionalAddress.GetProperty("country").GetString());
    }

    [Fact]
    public void Patient_FromFhirJsonR6WithContactRoleGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "contact" : [{
                    "role" : [{
                        "coding" : [{
                            "code" : "emergency"
                        }]
                    }]
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(patient);
        Assert.Single(patient.Contacts);
        Assert.Equal("emergency", patient.Contacts.First().Roles.First().Codings.First().Code.Value);
    }

    [Fact]
    public void Patient_FromFhirJsonR6WithAdditionalNameGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "contact" : [{
                    "additionalName" : [{
                        "family" : "Smith",
                        "given" : ["Jane"]
                    }]
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(patient);
        Assert.Single(patient.Contacts);
        Assert.Equal("Smith", patient.Contacts.First().AdditionalNames.First().Family?.Value);
        Assert.Equal("Jane", patient.Contacts.First().AdditionalNames.First().Given.First().Value);
    }

    [Fact]
    public void Patient_FromFhirJsonR6WithAdditionalAddressGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Patient",
                "contact" : [{
                    "additionalAddress" : [{
                        "city" : "Copenhagen",
                        "country" : "Denmark"
                    }]
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(patient);
        Assert.Single(patient.Contacts);
        Assert.Equal("Copenhagen", patient.Contacts.First().AdditionalAddresses.First().City?.Value);
        Assert.Equal("Denmark", patient.Contacts.First().AdditionalAddresses.First().Country?.Value);
    }
}
