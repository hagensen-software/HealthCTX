using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterStatusHistory;

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
            [new StatusHistory(
                new HistoryStatus("planned"),
                new HistoryPeriod(
                    new PeriodStart(periodStartDate),
                    new PeriodEnd(periodEndDate)))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var statusHistory = root
            .GetProperty("statusHistory")
            .EnumerateArray().First();

        Assert.Equal("planned", statusHistory.GetProperty("status").GetString());
        Assert.Equal(periodStartDateString, statusHistory.GetProperty("period").GetProperty("start").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "statusHistory": [{
                    "status": "planned",
                    "period": {
                        "start": "{{periodStartDateString}}",
                        "end": "{{periodEndDateString}}"
                    }
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("planned", encounter?.StatusHistories.First().Status?.Value);
        Assert.Equal(periodStartDate, encounter?.StatusHistories.First().Period?.Start.Value);
        Assert.Equal(periodEndDate, encounter?.StatusHistories.First().Period?.End.Value);
    }
}
