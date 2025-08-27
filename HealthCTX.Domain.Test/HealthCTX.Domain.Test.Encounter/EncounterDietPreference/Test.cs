using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterDietPreference;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterDietPreference(
                [new DietPreferenceCoding(
                    new DietPreferenceSystem(new Uri("http://terminology.hl7.org/CodeSystem/diet")),
                    new DietPreferenceCode("vegetarian"))])]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var typeCoding = root
            .GetProperty("dietPreference")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/diet", typeCoding.GetProperty("system").GetString());
        Assert.Equal("vegetarian", typeCoding.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "dietPreference" : [{
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/diet",
                        "code" : "vegetarian"
                    }]
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/diet", encounter?.DietPreferences.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("vegetarian", encounter?.DietPreferences.First().Codings.First().Code?.Value);
    }
}
