using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterActualPeriod;

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
            new EncounterActualPeriod(
                new PeriodStart(periodStartDate),
                new PeriodEnd(periodEndDate)));

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var period = root.GetProperty("actualPeriod");

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
                "actualPeriod" : {
                    "start" : "{{periodStartDateString}}",
                    "end" : "{{periodEndDateString}}"
                }
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(periodStartDate, encounter?.ActualPeriod?.Start?.Value);
        Assert.Equal(periodEndDate, encounter?.ActualPeriod?.End?.Value);
    }
}
