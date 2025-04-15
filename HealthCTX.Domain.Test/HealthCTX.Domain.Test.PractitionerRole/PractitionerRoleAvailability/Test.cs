using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleAvailability;

public class Test
{
    private readonly DateTimeOffset startDateTime = new(2023, 10, 1, 0, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset endDateTime = new(2023, 10, 31, 0, 0, 0, TimeSpan.Zero);

    private readonly TimeOnly startTime = new(12, 0);
    private readonly TimeOnly endTime = new(13, 30);

    [Fact]
    public void PractitionerRoleUsingAllDay_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleAvailability(
                new AvailableTime(
                    [new AvailableDaysOfWeek("mon")],
                    new AvailableAllDay(true),
                    null,
                    null),
                new NotAvailableTime(
                    new NotAvailableDescription("Not available"),
                    new NotAvailableDuring(
                        new NotAvailableStartTime(startDateTime),
                        new NotAvailableEndTime(endDateTime))))]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var availabilities = root.GetProperty("availability");
        var availability = availabilities.EnumerateArray().GetEnumerator().First();
        var availableTimes = availability.GetProperty("availableTime");
        var availableTime = availableTimes.EnumerateArray().GetEnumerator().First();
        var daysOfWeek = availableTime.GetProperty("daysOfWeek");
        var dayOfWeek = daysOfWeek.EnumerateArray().GetEnumerator().First();
        var notAvailabilities = availability.GetProperty("notAvailableTime");
        var notAvailable = notAvailabilities.EnumerateArray().GetEnumerator().First();

        Assert.Equal("mon", dayOfWeek.GetString());
        Assert.True(availableTime.GetProperty("allDay").GetBoolean());
        Assert.Equal("Not available", notAvailable.GetProperty("description").GetString());
        Assert.Equal(startDateTime, notAvailable.GetProperty("during").GetProperty("start").GetDateTimeOffset());
        Assert.Equal(endDateTime, notAvailable.GetProperty("during").GetProperty("end").GetDateTimeOffset());
    }

    [Fact]
    public void PractitionerRoleUsingAllDay_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"PractitionerRole",
                "availability":[{
                    "availableTime":[{
                        "allDay":true,
                        "daysOfWeek":["mon"]
                    }],
                    "notAvailableTime":[{
                        "description":"Not available",
                        "during":{
                            "start":"2023-10-01T00:00:00+00:00",
                            "end":"2023-10-31T00:00:00+00:00"
                        }
                    }]
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("mon", practitionerRole?.Availability.First().Available?.AvailableDays.First().Value);
        Assert.True(practitionerRole?.Availability.First().Available?.AllDay?.Value);
        Assert.Equal("Not available", practitionerRole?.Availability.First().NotAvailable?.Description.Value);
        Assert.Equal(startDateTime, practitionerRole?.Availability.First().NotAvailable?.During.StartTime.Value);
        Assert.Equal(endDateTime, practitionerRole?.Availability.First().NotAvailable?.During.EndTime.Value);
    }

    [Fact]
    public void PractitionerRoleUsingTimes_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleAvailability(
                new AvailableTime(
                    [new AvailableDaysOfWeek("mon")],
                    null,
                    new AvailableStartTime(startTime),
                    new AvailableEndTime(endTime)),
                null)]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var availabilities = root.GetProperty("availability");
        var availability = availabilities.EnumerateArray().GetEnumerator().First();
        var availableTimes = availability.GetProperty("availableTime");
        var availableTime = availableTimes.EnumerateArray().GetEnumerator().First();
        var daysOfWeek = availableTime.GetProperty("daysOfWeek");
        var dayOfWeek = daysOfWeek.EnumerateArray().GetEnumerator().First();

        Assert.Equal("mon", dayOfWeek.GetString());
        Assert.Equal(startTime, TimeOnly.Parse(availableTime.GetProperty("availableStartTime").GetString()!));
        Assert.Equal(endTime, TimeOnly.Parse(availableTime.GetProperty("availableEndTime").GetString()!));
    }

    [Fact]
    public void PractitionerRoleUsingTimes_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"PractitionerRole",
                "availability":[{
                    "availableTime":[{
                        "daysOfWeek":["mon"],
                        "availableStartTime":"12:00:00",
                        "availableEndTime":"13:30:00"
                    }]
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("mon", practitionerRole?.Availability.First().Available?.AvailableDays.First().Value);
        Assert.Equal(startTime, practitionerRole?.Availability.First().Available?.StartTime?.Value);
        Assert.Equal(endTime, practitionerRole?.Availability.First().Available?.EndTime?.Value);
    }
}
