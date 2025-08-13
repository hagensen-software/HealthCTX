using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterParticipantR4;

public class Test
{
    private readonly DateTimeOffset periodStartDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string periodStartDateString = "2024-02-14T13:42:00+01:00";
    private readonly DateTimeOffset periodEndDate = new(2024, 1, 31, 0, 0, 0, new TimeSpan(1, 0, 0));
    private readonly string periodEndDateString = "2024-01-31T00:00:00+01:00";

    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterParticipant(
                [new ParticipantType(
                    [new ParticipantTypeCoding(
                        new Code("PPRF"),
                        new CodeSystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-ParticipationType")))])],
                new ParticipantPeriod(
                    new PeriodStart(periodStartDate),
                    new PeriodEnd(periodEndDate)),
                new ParticipantIndividual(
                    new IndividualReference("Practitioner/123")))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var participant = root
            .GetProperty("participant")
            .EnumerateArray().First();
        var type = participant.GetProperty("type")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First();
        var period = participant.GetProperty("period");
        var individual = participant.GetProperty("individual")
            .GetProperty("reference");

        Assert.Equal("PPRF", type.GetProperty("code").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ParticipationType", type.GetProperty("system").GetString());
        Assert.Equal(periodStartDateString, period.GetProperty("start").GetString());
        Assert.Equal(periodEndDateString, period.GetProperty("end").GetString());
        Assert.Equal("Practitioner/123", individual.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "participant" : [{
                    "type" : [{
                        "coding" : [{
                            "code" : "PPRF",
                            "system" : "http://terminology.hl7.org/CodeSystem/v3-ParticipationType"
                        }]
                    }],
                    "period" : {
                        "start" : "{{periodStartDateString}}",
                        "end" : "{{periodEndDateString}}"
                    },
                    "individual" : {
                        "reference" : "Practitioner/123"
                    }
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("PPRF", encounter?.Participants.First().Types.First().Codings.First().Code?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ParticipationType", encounter?.Participants.First().Types.First().Codings.First().System?.Value.OriginalString);
        Assert.Equal(periodStartDate, encounter?.Participants.First().Period.Start?.Value);
        Assert.Equal(periodEndDate, encounter?.Participants.First().Period.End?.Value);
        Assert.Equal("Practitioner/123", encounter?.Participants.First().Individual.Reference?.Value);
    }
}
