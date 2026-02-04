using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationHoursOfOperationR6;

public class Test
{
    private readonly TimeOnly startTime = new(9, 0);
    private readonly TimeOnly endTime = new(17, 0);

    [Fact]
    public void Location_ToFhirJsonR6WithHoursOfOperationGeneratesJsonString()
    {
        var location = new Location(
            new HoursOfOperation(
                [new AvailableTime(
                    [new AvailableDaysOfWeek("mon"), new AvailableDaysOfWeek("tue")],
                    new AvailableAllDay(false),
                    new AvailableStartTime(startTime),
                    new AvailableEndTime(endTime))],
                [new NotAvailableTime(new NotAvailableDescription("Public holidays"))]));

        (var jsonString, var outcomes) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var hoursOfOperation = root.GetProperty("hoursOfOperation");
        var availableTime = hoursOfOperation
            .GetProperty("availableTime")
            .EnumerateArray().First();
        var notAvailableTime = hoursOfOperation
            .GetProperty("notAvailableTime")
            .EnumerateArray().First();

        var daysOfWeek = availableTime.GetProperty("daysOfWeek").EnumerateArray().ToList();
        Assert.Equal(2, daysOfWeek.Count);
        Assert.Equal("mon", daysOfWeek[0].GetString());
        Assert.Equal("tue", daysOfWeek[1].GetString());
        Assert.False(availableTime.GetProperty("allDay").GetBoolean());
        Assert.Equal(startTime, TimeOnly.Parse(availableTime.GetProperty("availableStartTime").GetString()!));
        Assert.Equal(endTime, TimeOnly.Parse(availableTime.GetProperty("availableEndTime").GetString()!));
        Assert.Equal("Public holidays", notAvailableTime.GetProperty("description").GetString());
    }

    [Fact]
    public void Location_ToFhirJsonR6WithoutHoursOfOperationGeneratesJsonString()
    {
        var location = new Location(null);

        (var jsonString, var outcomes) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.False(root.TryGetProperty("hoursOfOperation", out _));
    }

    [Fact]
    public void Location_FromFhirJsonR6WithHoursOfOperationGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Location",
                "hoursOfOperation" : {
                    "availableTime" : [{
                        "daysOfWeek" : ["mon", "tue"],
                        "allDay" : false,
                        "availableStartTime" : "{{startTime}}",
                        "availableEndTime" : "{{endTime}}"
                    }],
                    "notAvailableTime" : [{
                        "description" : "Public holidays"
                    }]
                }
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(location);
        Assert.NotNull(location.HoursOfOperation);
        Assert.Single(location.HoursOfOperation.AvailableTimes);

        var availableTime = location.HoursOfOperation.AvailableTimes.First();
        Assert.Equal(2, availableTime.DaysOfWeek.Count);
        Assert.Equal("mon", availableTime.DaysOfWeek[0].Value);
        Assert.Equal("tue", availableTime.DaysOfWeek[1].Value);
        Assert.False(availableTime.AllDay?.Value);
        Assert.Equal(startTime, availableTime.AvailableStartTime?.Value);
        Assert.Equal(endTime, availableTime.AvailableEndTime?.Value);

        Assert.Single(location.HoursOfOperation.NotAvailableTimes);
        Assert.Equal("Public holidays", location.HoursOfOperation.NotAvailableTimes.First().Description.Value);
    }

    [Fact]
    public void Location_FromFhirJsonR6WithoutHoursOfOperationGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location"
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(location);
        Assert.Null(location.HoursOfOperation);
    }
}
