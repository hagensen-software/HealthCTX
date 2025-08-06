using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterPriority;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new EncounterPriority(
                [new PriorityCoding(
                    new PrioritySystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-ActPriority")),
                    new PriorityCode("A"))]));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var priorityCoding = root
            .GetProperty("priority")
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ActPriority", priorityCoding.GetProperty("system").GetString());
        Assert.Equal("A", priorityCoding.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "priority" : {
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/v3-ActPriority",
                        "code" : "A"
                    }]
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ActPriority", encounter?.Priority?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("A", encounter?.Priority?.Codings.First().Code?.Value);
    }
}
