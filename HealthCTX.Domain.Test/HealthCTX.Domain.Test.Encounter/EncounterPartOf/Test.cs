using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterPartOf;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new PartOfEncounter(
                new EncounterReference("Encounter/123")));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var episodeOfCareReference = root
            .GetProperty("partOf")
            .GetProperty("reference");

        Assert.Equal("Encounter/123", episodeOfCareReference.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "partOf" : {
                    "reference" : "Encounter/123"
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Encounter/123", encounter?.PartOf?.Reference?.Value);
    }
}

