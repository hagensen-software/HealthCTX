using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterEpisodeOfCare;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EpisodeOfCare(
                new EpisodeOfCareReference("EpisodeOCare/123"))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var episodeOfCareReference = root
            .GetProperty("episodeOfCare")
            .EnumerateArray().First()
            .GetProperty("reference");

        Assert.Equal("EpisodeOCare/123", episodeOfCareReference.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "episodeOfCare" : [{
                    "reference" : "EpisodeOCare/123"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("EpisodeOCare/123", encounter?.EpisodeOfCares.First().Reference?.Value);
    }
}
