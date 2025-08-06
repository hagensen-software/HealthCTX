using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterBasedOn;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new BasedOn(
                new BasedOnReference("ServiceRequest/123"))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var episodeOfCareReference = root
            .GetProperty("basedOn")
            .EnumerateArray().First()
            .GetProperty("reference");

        Assert.Equal("ServiceRequest/123", episodeOfCareReference.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "basedOn" : [{
                    "reference" : "ServiceRequest/123"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("ServiceRequest/123", encounter?.BasedOn.First().Reference?.Value);
    }
}
