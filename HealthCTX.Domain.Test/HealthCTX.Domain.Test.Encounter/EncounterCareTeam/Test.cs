using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterCareTeam;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new CareTeam(
                new CareTeamReference("CareTeam/123"))]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var episodeOfCareReference = root
            .GetProperty("careTeam")
            .EnumerateArray().First()
            .GetProperty("reference");

        Assert.Equal("CareTeam/123", episodeOfCareReference.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "careTeam" : [{
                    "reference" : "CareTeam/123"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("CareTeam/123", encounter?.CareTeams.First().Reference?.Value);
    }
}

