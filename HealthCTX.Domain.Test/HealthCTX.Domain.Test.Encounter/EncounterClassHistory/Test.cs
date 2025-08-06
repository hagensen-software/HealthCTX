using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterClassHistory;

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
            [new ClassHistory(
                new ClassCoding(
                    new ClassSystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-ActCode")),
                    new ClassCode("IMP")),
                new HistoryPeriod(
                    new PeriodStart(periodStartDate),
                    new PeriodEnd(periodEndDate)))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var classHistory = root
            .GetProperty("classHistory")
            .EnumerateArray().First();

        JsonElement coding = classHistory.GetProperty("class");

        Assert.Equal("IMP", coding.GetProperty("code").GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ActCode", coding.GetProperty("system").GetString());
        Assert.Equal(periodStartDateString, classHistory.GetProperty("period").GetProperty("start").GetString());
        Assert.Equal(periodEndDateString, classHistory.GetProperty("period").GetProperty("end").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "classHistory": [{
                    "class": {
                        "code": "IMP",
                        "system": "http://terminology.hl7.org/CodeSystem/v3-ActCode"
                    },
                    "period": {
                        "start": "{{periodStartDateString}}",
                        "end": "{{periodEndDateString}}"
                    }
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("IMP", encounter?.StatusHistories.First().Class?.Code?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/v3-ActCode", encounter?.StatusHistories.First().Class?.System?.Value.ToString());
        Assert.Equal(periodStartDate, encounter?.StatusHistories.First().Period?.Start?.Value);
        Assert.Equal(periodEndDate, encounter?.StatusHistories.First().Period?.End?.Value);
    }
}
