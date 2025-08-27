using HealthCTX.Domain.Test.Encounter.EncounterPartOf;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterServiceProvider;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new Organization(
                new OrganizationReference("Organization/123")));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var episodeOfCareReference = root
            .GetProperty("serviceProvider")
            .GetProperty("reference");

        Assert.Equal("Organization/123", episodeOfCareReference.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "serviceProvider" : {
                    "reference" : "Organization/123"
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Organization/123", encounter?.ServiceProvider?.Reference?.Value);
    }
}

