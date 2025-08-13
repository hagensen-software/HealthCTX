using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterAdmission;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new EncounterAdmission(
                new HospitalizationPreAdmissionIdentifier(
                    new IdentifierSystem(new Uri("http://some-identifier-system.org")),
                    new IdentifierValue("1234567890")),
                new HospitalizationOrigin(
                    new OriginReferenceReference("Organization/123")),
                new HospitalizationAdmitSource(
                    [new AdmitSourceCoding(
                        new AdmitSourceSystem(new Uri("http://terminology.hl7.org/CodeSystem/admit-source")),
                        new AdmitSourceCode("hosp-trans"))]),
                new HospitalizationReAdmission(
                    [new ReAdmissionCoding(
                        new ReAdmissionSystem(new Uri("http://terminology.hl7.org/CodeSystem/v2-0092")),
                        new ReAdmissionCode("R"))]),
                new HospitalizationDestination(
                    new DestinationReferenceReference("Organization/123")),
                new HospitalizationDischargeDisposition(
                    [new DischargeDispositionCoding(
                        new DischargeDispositionSystem(new Uri("http://terminology.hl7.org/CodeSystem/discharge-disposition")),
                        new DischargeDispositionCode("home"))])
                ));

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("status");
        var preAdmissionIdentifier = root
            .GetProperty("admission")
            .GetProperty("preAdmissionIdentifier");
        var admissionOrigin = root
            .GetProperty("admission")
            .GetProperty("origin");
        var admitSource = root
            .GetProperty("admission")
            .GetProperty("admitSource")
            .GetProperty("coding")
            .EnumerateArray().First();
        var reAdmission = root
            .GetProperty("admission")
            .GetProperty("reAdmission")
            .GetProperty("coding")
            .EnumerateArray().First();
        var destination = root
            .GetProperty("admission")
            .GetProperty("destination");
        var dischargeDisposition = root
            .GetProperty("admission")
            .GetProperty("dischargeDisposition")
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://some-identifier-system.org", preAdmissionIdentifier.GetProperty("system").GetString());
        Assert.Equal("1234567890", preAdmissionIdentifier.GetProperty("value").GetString());
        Assert.Equal("Organization/123", admissionOrigin.GetProperty("reference").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/admit-source", admitSource.GetProperty("system").GetString());
        Assert.Equal("hosp-trans", admitSource.GetProperty("code").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0092", reAdmission.GetProperty("system").GetString());
        Assert.Equal("R", reAdmission.GetProperty("code").GetString());
        Assert.Equal("Organization/123", destination.GetProperty("reference").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/discharge-disposition", dischargeDisposition.GetProperty("system").GetString());
        Assert.Equal("home", dischargeDisposition.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "admission" : {
                    "preAdmissionIdentifier" : {
                        "system" : "http://some-identifier-system.org",
                        "value" : "1234567890"
                    },
                    "origin" : {
                        "reference" : "Organization/123"
                    },
                    "admitSource" : {
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/admit-source",
                            "code" : "hosp-trans"
                        }]
                    },
                    "reAdmission" : {
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/v2-0092",
                            "code" : "R"
                        }]
                    },
                    "dietPreference" : [{
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/diet",
                            "code" : "vegetarian"
                        }]
                    }],
                    "specialCourtesy" : [{
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy",
                            "code" : "EXT"
                        }]
                    }],
                    "specialArrangement" : [{
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/encounter-special-arrangements",
                            "code" : "wheel"
                        }]
                    }],
                    "destination" : {
                        "reference" : "Organization/123"
                    },
                    "dischargeDisposition" : {
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/discharge-disposition",
                            "code" : "home"
                        }]
                    }
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://some-identifier-system.org", encounter?.Admission?.PreAdmissionIdentifier?.System?.Value.OriginalString);
        Assert.Equal("1234567890", encounter?.Admission?.PreAdmissionIdentifier?.Value?.Value);
        Assert.Equal("Organization/123", encounter?.Admission?.Origin?.Reference?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/admit-source", encounter?.Admission?.AdmitSource?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("hosp-trans", encounter?.Admission?.AdmitSource?.Codings.First().Code?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0092", encounter?.Admission?.ReAdmission?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("R", encounter?.Admission?.ReAdmission?.Codings.First().Code?.Value);
        Assert.Equal("Organization/123", encounter?.Admission?.Destination?.Reference?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/discharge-disposition", encounter?.Admission?.DischargeDisposition?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("home", encounter?.Admission?.DischargeDisposition?.Codings.First().Code?.Value);
    }
}
