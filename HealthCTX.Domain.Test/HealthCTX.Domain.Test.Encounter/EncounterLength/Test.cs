using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterLength;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new EncounterLength(
                new QuantityValue(90),
                new QuantityUnit("min")));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var status = root.GetProperty("status");
        var length = root.GetProperty("length");

        Assert.Equal(90, length.GetProperty("value").GetInt32());
        Assert.Equal("min", length.GetProperty("unit").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "length" : {
                    "value" : 90,
                    "unit" : "min"
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(90, encounter?.Length?.Value?.Value);
        Assert.Equal("min", encounter?.Length?.Unit?.Value);
    }
}
