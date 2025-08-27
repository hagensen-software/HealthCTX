using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterPeriod;

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
            new EncounterPeriod(
                new PeriodStart(periodStartDate),
                new PeriodEnd(periodEndDate)));

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var period = root.GetProperty("period");

        Assert.Equal(periodStartDateString, period.GetProperty("start").GetString());
        Assert.Equal(periodEndDateString, period.GetProperty("end").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "period" : {
                    "start" : "{{periodStartDateString}}",
                    "end" : "{{periodEndDateString}}"
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(periodStartDate, encounter?.Period?.Start?.Value);
        Assert.Equal(periodEndDate, encounter?.Period?.End?.Value);
    }
}
