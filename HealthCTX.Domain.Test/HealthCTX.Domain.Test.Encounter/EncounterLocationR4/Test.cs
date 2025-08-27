using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterLocationR4;

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
            [new EncounterLocation(
                new LocationLocation(
                    new LocationReference("Location/123")),
            new LocationStatus("planned"),
            new LocationPhysicalType(
                [new PhysicalTypeCoding(
                    new PhysicalTypeSystem(new Uri("http://terminology.hl7.org/CodeSystem/location-physical-type")),
                    new PhysicalTypeCode("bu"))]),
            new LocationPeriod(
                new PeriodStart(periodStartDate),
                new PeriodEnd(periodEndDate)))]);

        (var jsonString, _) = encounter.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var location = root
            .GetProperty("location")
            .EnumerateArray().First();
        var locationReference = location
            .GetProperty("location")
            .GetProperty("reference");
        var status = location.GetProperty("status");
        var form = location
            .GetProperty("physicalType")
            .GetProperty("coding")
            .EnumerateArray().First();
        var period = location.GetProperty("period");

        Assert.Equal("Location/123", locationReference.GetString());
        Assert.Equal("planned", status.GetString());
        Assert.Equal("http://terminology.hl7.org/CodeSystem/location-physical-type", form.GetProperty("system").GetString());
        Assert.Equal("bu", form.GetProperty("code").GetString());
        Assert.Equal(periodStartDateString, period.GetProperty("start").GetString());
        Assert.Equal(periodEndDateString, period.GetProperty("end").GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "location" : [{
                    "location" : {
                        "reference" : "Location/123"
                    },
                    "status" : "planned",
                    "physicalType" : {
                        "coding" : [{
                            "system" : "http://terminology.hl7.org/CodeSystem/location-physical-type",
                            "code" : "bu"
                        }]
                    },
                    "period" : {
                        "start" : "2024-02-14T13:42:00+01:00",
                        "end" : "2024-01-31T00:00:00+01:00"
                    }
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Location/123", encounter?.Locations.First().Location.Reference?.Value);
        Assert.Equal("planned", encounter?.Locations.First().Status?.Value);
        Assert.Equal("http://terminology.hl7.org/CodeSystem/location-physical-type", encounter?.Locations.First().PhysicalType?.Codings.First().System?.Value.OriginalString);
        Assert.Equal("bu", encounter?.Locations.First().PhysicalType?.Codings.First().Code?.Value);
        Assert.Equal(periodStartDate, encounter?.Locations.First().Period?.Start?.Value);
        Assert.Equal(periodEndDate, encounter?.Locations.First().Period?.End?.Value);
    }
}
