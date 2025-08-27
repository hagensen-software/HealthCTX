using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterClassR4;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterClass(
                new ClassSystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-ActCode")),
                new ClassCode("IMP"))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var encounterClass = root
            .GetProperty("class")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ActCode", encounterClass.GetProperty("system").GetString());
        Assert.Equal("IMP", encounterClass.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "class": [{
                    "system": "http://terminology.hl7.org/CodeSystem/v3-ActCode",
                    "code": "IMP"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ActCode", encounter?.Classes.First().System?.Value.ToString());
        Assert.Equal("IMP", encounter?.Classes.First().Code?.Value);
    }
}
