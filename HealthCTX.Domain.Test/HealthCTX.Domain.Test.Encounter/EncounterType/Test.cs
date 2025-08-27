using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterType;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterType(
                [new TypeCoding(
                    new TypeSystem(new Uri("http://terminology.hl7.org/CodeSystem/encounter-type")),
                    new TypeCode("ADMS"))])]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var typeCoding = root
            .GetProperty("type")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-type", typeCoding.GetProperty("system").GetString());
        Assert.Equal("ADMS", typeCoding.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "type" : [
                    {
                        "coding" : [
                            {
                                "system" : "http://terminology.hl7.org/CodeSystem/encounter-type",
                                "code" : "ADMS"
                            }
                        ]
                    }
                ]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-type", encounter?.Types.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal("ADMS", encounter?.Types.First().Codings.First().Code?.Value);
    }
}
