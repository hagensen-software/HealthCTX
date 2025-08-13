using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterHospitalization;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new EncounterHospitalization(
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
                [new HospitalizationDietPreference(
                    [new DietPreferenceCoding(
                        new DietPreferenceSystem(new Uri("http://terminology.hl7.org/CodeSystem/diet")),
                        new DietPreferenceCode("vegetarian"))])],
                [new HospitalizationSpecialCourtesy(
                    [new SpecialCourtesyCoding(
                        new SpecialCourtesySystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy")),
                        new SpecialCourtesyCode("EXT"))])],
                [new HospitalizationSpecialArrangement(
                    [new SpecialArrangementCoding(
                        new SpecialArrangementSystem(new Uri("http://terminology.hl7.org/CodeSystem/encounter-special-arrangements")),
                        new SpecialArrangementCode("wheel"))])],
                new HospitalizationDestination(
                    new DestinationReferenceReference("Organization/123")),
                new HospitalizationDischargeDisposition(
                    [new DischargeDispositionCoding(
                        new DischargeDispositionSystem(new Uri("http://terminology.hl7.org/CodeSystem/discharge-disposition")),
                        new DischargeDispositionCode("home"))])
                ));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("status");
        var preAdmissionIdentifier = root
            .GetProperty("hospitalization")
            .GetProperty("preAdmissionIdentifier");
        var hospitalizationOrigin = root
            .GetProperty("hospitalization")
            .GetProperty("origin");
        var admitSource = root
            .GetProperty("hospitalization")
            .GetProperty("admitSource")
            .GetProperty("coding")
            .EnumerateArray().First();
        var reAdmission = root
            .GetProperty("hospitalization")
            .GetProperty("reAdmission")
            .GetProperty("coding")
            .EnumerateArray().First();
        var dietPreference = root
            .GetProperty("hospitalization")
            .GetProperty("dietPreference")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();
        var specialCourtesy = root
            .GetProperty("hospitalization")
            .GetProperty("specialCourtesy")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();
        var specialArrangement = root
            .GetProperty("hospitalization")
            .GetProperty("specialArrangement")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();
        var destination = root
            .GetProperty("hospitalization")
            .GetProperty("destination");
        var dischargeDisposition = root
            .GetProperty("hospitalization")
            .GetProperty("dischargeDisposition")
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://some-identifier-system.org", preAdmissionIdentifier.GetProperty("system").GetString());
        Assert.Equal("1234567890", preAdmissionIdentifier.GetProperty("value").GetString());
        Assert.Equal("Organization/123", hospitalizationOrigin.GetProperty("reference").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/admit-source", admitSource.GetProperty("system").GetString());
        Assert.Equal("hosp-trans", admitSource.GetProperty("code").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0092", reAdmission.GetProperty("system").GetString());
        Assert.Equal("R", reAdmission.GetProperty("code").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/diet", dietPreference.GetProperty("system").GetString());
        Assert.Equal("vegetarian", dietPreference.GetProperty("code").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy", specialCourtesy.GetProperty("system").GetString());
        Assert.Equal("EXT", specialCourtesy.GetProperty("code").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-special-arrangements", specialArrangement.GetProperty("system").GetString());
        Assert.Equal("wheel", specialArrangement.GetProperty("code").GetString());
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
                "hospitalization" : {
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

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://some-identifier-system.org", encounter?.Hospitalization?.PreAdmissionIdentifier?.System?.Value.OriginalString);
        Assert.Equal("1234567890", encounter?.Hospitalization?.PreAdmissionIdentifier?.Value?.Value);
        Assert.Equal("Organization/123", encounter?.Hospitalization?.Origin?.Reference?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/admit-source", encounter?.Hospitalization?.AdmitSource?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("hosp-trans", encounter?.Hospitalization?.AdmitSource?.Codings.First().Code?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v2-0092", encounter?.Hospitalization?.ReAdmission?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("R", encounter?.Hospitalization?.ReAdmission?.Codings.First().Code?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/diet", encounter?.Hospitalization?.DietPreferences.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("vegetarian", encounter?.Hospitalization?.DietPreferences.First().Codings.First().Code?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-EncounterSpecialCourtesy", encounter?.Hospitalization?.SpecialCourtesies.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("EXT", encounter?.Hospitalization?.SpecialCourtesies.First().Codings.First().Code?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-special-arrangements", encounter?.Hospitalization?.SpecialArrangements.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("wheel", encounter?.Hospitalization?.SpecialArrangements.First().Codings.First().Code?.Value);
        Assert.Equal("Organization/123", encounter?.Hospitalization?.Destination?.Reference?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/discharge-disposition", encounter?.Hospitalization?.DischargeDisposition?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("home", encounter?.Hospitalization?.DischargeDisposition?.Codings.First().Code?.Value);
    }
}
