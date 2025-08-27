using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterDiagnosisR4;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterDiagnosis(
                [new ConditionReference(
                    new ConditionReferenceReference("Condition/123"))],
                new DiagnosisUse(
                    [new DiagnosisUseCoding(
                        new DiagnosisUseSystem(new Uri("http://terminology.hl7.org/CodeSystem/diagnosis-role")),
                        new DiagnosisUseCode("AD"))]),
                new DiagnosisRank(1))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("status");
        var diagnosis = root
            .GetProperty("diagnosis")
            .EnumerateArray().First();
        var condition = diagnosis
            .GetProperty("condition")
            .EnumerateArray().First();
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
                        "reference" : "Condition/123"
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

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Condition/123", encounter?.Diagnosis.First().Conditions.First().Reference.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/diagnosis-role", encounter?.Diagnosis.First().Use?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("AD", encounter?.Diagnosis.First().Use?.Codings.First().Code?.Value);
        Assert.Equal(1u, encounter?.Diagnosis.First().Rank?.Value);
    }
}
