using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.SubjectStatus;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new SubjectStatus(
                [new SubjectSStatusCoding(
                    new SubjectSStatusSystem(new Uri("http://terminology.hl7.org/CodeSystem/encounter-subject-status")),
                    new SubjectSStatusCode("arrived"))]));

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var subjectStatus = root
            .GetProperty("subjectStatus")
            .GetProperty("coding")
            .EnumerateArray().First();

        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-subject-status", subjectStatus.GetProperty("system").GetString());
        Assert.Equal("arrived", subjectStatus.GetProperty("code").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "subjectStatus" : {
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/encounter-subject-status",
                        "code" : "arrived"
                    }]
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/encounter-subject-status", encounter?.SubjectStatus?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("arrived", encounter?.SubjectStatus?.Codings.First().Code?.Value);
    }
}
