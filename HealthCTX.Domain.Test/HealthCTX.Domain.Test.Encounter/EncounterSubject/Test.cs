using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterSubject;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new Subject(
                new SubjectReference("Patient/123")));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var subject = root
            .GetProperty("subject")
            .GetProperty("reference");

        Assert.Equal("Patient/123", subject.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "subject" : {
                    "reference" : "Patient/123"
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Patient/123", encounter?.Subject?.Reference?.Value);
    }
}
