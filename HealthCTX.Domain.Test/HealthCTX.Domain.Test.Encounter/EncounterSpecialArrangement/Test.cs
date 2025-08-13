using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterSpecialArrangement;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterSpecialArrangement(
                [new SpecialArrangementCoding(
                    new SpecialArrangementSystem(new Uri("http://terminology.hl7.org/CodeSystem/encounter-special-arrangements")),
                    new SpecialArrangementCode("wheel"))])]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var typeCoding = root
            .GetProperty("specialArrangement")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-special-arrangements", typeCoding.GetProperty("system").GetString());
        Assert.Equal("wheel", typeCoding.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "specialArrangement" : [{
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/encounter-special-arrangements",
                        "code" : "wheel"
                    }]
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-special-arrangements", encounter?.SpecialArrangements.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("wheel", encounter?.SpecialArrangements.First().Codings.First().Code?.Value);
    }
}
