using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterPlannedEndDate;

public class Test
{
    private readonly DateTimeOffset date = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string dateString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            new PlannedEndDate(date));

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var plannedStartDate = root.GetProperty("plannedEndDate");

        Assert.Equal(dateString, plannedStartDate.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "plannedEndDate" : "{{dateString}}"
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(date, encounter?.PlannedEndDate?.Value);
    }
}
