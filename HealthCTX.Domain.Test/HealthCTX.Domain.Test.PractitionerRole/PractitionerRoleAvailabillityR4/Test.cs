using HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleAvailability;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleAvailabillityR4;

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
            [new AvailableTime(
                [new AvailableDaysOfWeek("mon")],
                new AvailableAllDay(true),
                null,
                null)],
            [new NotAvailableTime(
                new NotAvailableDescription("Not available"),
                new NotAvailableDuring(
                    new NotAvailableStartTime(startDateTime),
                    new NotAvailableEndTime(endDateTime)))],
            new PractitionerRoleAvailabilityExceptions("Not available"));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var availableTimes = root.GetProperty("availableTime");
        var availableTime = availableTimes.EnumerateArray().GetEnumerator().First();
        var daysOfWeek = availableTime.GetProperty("daysOfWeek");
        var dayOfWeek = daysOfWeek.EnumerateArray().GetEnumerator().First();
        var notAvailabilities = root.GetProperty("notAvailable");
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
                "availableTime":[{
                    "allDay":true,
                    "daysOfWeek":["mon"]
                }],
                "notAvailable":[{
                    "description":"Not available",
                    "during":{
                        "start":"2023-10-01T00:00:00+00:00",
                        "end":"2023-10-31T00:00:00+00:00"
                    }
                }],
                "availabilityExceptions":["Not available"]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("mon", practitionerRole?.Available.First().AvailableDays.First().Value);
        Assert.True(practitionerRole?.Available.First().AllDay?.Value);
        Assert.Equal("Not available", practitionerRole?.NotAvailable.First().Description.Value);
        Assert.Equal(startDateTime, practitionerRole?.NotAvailable.First().During.StartTime.Value);
        Assert.Equal(endDateTime, practitionerRole?.NotAvailable.First().During.EndTime.Value);
        Assert.Equal("Not available", practitionerRole?.AvailabilityExceptions?.Value);
    }

    [Fact]
    public void PractitionerRoleUsingTimes_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new AvailableTime(
                [new AvailableDaysOfWeek("mon")],
                null,
                new AvailableStartTime(startTime),
                new AvailableEndTime(endTime))],
            [],
            new PractitionerRoleAvailabilityExceptions("Not available"));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var availableTimes = root.GetProperty("availableTime");
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
                "availableTime":[{
                    "daysOfWeek":["mon"],
                    "availableStartTime":"12:00:00",
                    "availableEndTime":"13:30:00"
                }],
                "availabilityExceptions":["Not available"]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("mon", practitionerRole?.Available.First().AvailableDays.First().Value);
        Assert.Equal(startTime, practitionerRole?.Available.First().StartTime?.Value);
        Assert.Equal(endTime, practitionerRole?.Available.First().EndTime?.Value);
    }
}
