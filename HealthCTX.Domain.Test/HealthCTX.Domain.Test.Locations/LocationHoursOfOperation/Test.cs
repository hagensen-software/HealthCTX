using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationHoursOfOperation;

public class Test
{
    private readonly TimeOnly startTime = new(12, 0);
    private readonly TimeOnly endTime = new(13, 30);

    [Fact]
    public void Location_ToFhirR4JsonGeneratesJsonString()
    {
        var location = new LocationR4(
            [new LocationHoursOfOperationR4(
                [new OperationDaysOfWeek("mon")],
                new OperationAllDay(true),
                new OperationOpeningTime(startTime),
                new OperationClosingTime(endTime))]);

        (var jsonString, _) = LocationR4FhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var hoursOfOperation = root
            .GetProperty("hoursOfOperation")
            .EnumerateArray().First();
        var dayOfWeek = hoursOfOperation
            .GetProperty("daysOfWeek")
            .EnumerateArray().First();

        Assert.Equal("mon", dayOfWeek.GetString());
        Assert.True(hoursOfOperation.GetProperty("allDay").GetBoolean());
        Assert.Equal(startTime, TimeOnly.Parse(hoursOfOperation.GetProperty("openingTime").GetString()!));
        Assert.Equal(endTime, TimeOnly.Parse(hoursOfOperation.GetProperty("closingTime").GetString()!));
    }

    [Fact]
    public void Location_FromFhirR4JsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType":"PractitionerRole",
                "hoursOfOperation":[{
                    "daysOfWeek":["mon"],
                    "allDay":true,
                    "openingTime":"{{startTime}}",
                    "closingTime":"{{endTime}}"
                }]
            }
            """;

        (var location, var outcomes) = LocationR4FhirJsonMapper.ToLocationR4(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("mon", location?.HoursOfOperation.First().DaysOfWeek.First().Value);
        Assert.True(location?.HoursOfOperation.First().AllDay?.Value);
        Assert.Equal(startTime, location?.HoursOfOperation.First().OpeningTime.Value);
        Assert.Equal(endTime, location?.HoursOfOperation.First().ClosingTime.Value);
    }

    [Fact]
    public void Location_ToFhirR5JsonGeneratesJsonString()
    {
        var location = new LocationR5(
            [new LocationHoursOfOperationR5(
                new LocationAvailable(
                    new AvailableStartTime(startTime)),
                new LocationNotAvailable(
                    new NotAvailableDescription("Not available")))]);

        (var jsonString, _) = LocationR5FhirJsonMapper.ToFhirJsonString(location, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var hoursOfOperation = root
            .GetProperty("hoursOfOperation")
            .EnumerateArray().First();
        var availableStartTime = hoursOfOperation
            .GetProperty("availableTime")
            .EnumerateArray().First()
            .GetProperty("availableStartTime");
        var notAvailableDescription = hoursOfOperation
            .GetProperty("notAvailableTime")
            .EnumerateArray().First()
            .GetProperty("description");

        Assert.Equal(startTime, TimeOnly.Parse(availableStartTime.GetString()!));
        Assert.Equal("Not available", notAvailableDescription.GetString());
    }

    [Fact]
    public void Location_FromFhirR5JsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType":"Location",
                "hoursOfOperation":[{
                    "availableTime":[{
                        "availableStartTime":"{{startTime}}"
                    }],
                    "notAvailableTime":[{
                        "description":"Not available"
                    }]
                }]
            }
            """;

        (var location, var outcomes) = LocationR5FhirJsonMapper.ToLocationR5(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(startTime, location?.HoursOfOperation.First().Available.AvailableStartTime.Value);
        Assert.Equal("Not available", location?.HoursOfOperation.First().NotAvailable.Description.Value);
    }
}
