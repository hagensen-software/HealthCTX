using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterDiagnosisR5;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterDiagnosis(
                [new DiagnosisReference(
                    new ConditionReference(
                        new ReferenceReference("Condition/123")))],
                new DiagnosisUse(
                    [new DiagnosisUseCoding(
                        new DiagnosisUseSystem(new Uri("http://terminology.hl7.org/CodeSystem/diagnosis-role")),
                        new DiagnosisUseCode("AD"))]))]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("status");
        var diagnosis = root
            .GetProperty("diagnosis")
            .EnumerateArray().First();
        var condition = diagnosis
            .GetProperty("condition")
            .EnumerateArray().First()
            .GetProperty("reference");
        var useCoding = diagnosis
            .GetProperty("use")
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("Condition/123", condition.GetProperty("reference").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/diagnosis-role", useCoding.GetProperty("system").GetString());
        Assert.Equal("AD", useCoding.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "diagnosis" : [{
                    "condition" : [{
                        "reference" : {
                            "reference" : "Condition/123"
                        }
                    }],
                    "use" : {
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/diagnosis-role",
                            "code" : "AD"
                        }]
                    },
                    "rank" : 1
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Condition/123", encounter?.Diagnosis.First().Conditions.First().Reference.Reference.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/diagnosis-role", encounter?.Diagnosis.First().Use?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("AD", encounter?.Diagnosis.First().Use?.Codings.First().Code?.Value);
    }
}
