using HealthCTX.Domain.Availabilities;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationHoursOfOperationR6;

public record AvailableDaysOfWeek(string Value) : IAvailabilityAvailableDaysOfWeek;
public record AvailableAllDay(bool Value) : IAvailabilityAvailableAllDay;
public record AvailableStartTime(TimeOnly Value) : IAvailabilityAvailableStartTime;
public record AvailableEndTime(TimeOnly Value) : IAvailabilityAvailableEndTime;

public record AvailableTime(
    ImmutableList<AvailableDaysOfWeek> DaysOfWeek,
    AvailableAllDay? AllDay,
    AvailableStartTime? AvailableStartTime,
    AvailableEndTime? AvailableEndTime) : IAvailabilityAvailable;

public record NotAvailableDescription(string Value) : IAvailabilityNotAvailableDescription;
public record NotAvailableTime(NotAvailableDescription Description) : IAvailabilityNotAvailable;

public record HoursOfOperation(
    ImmutableList<AvailableTime> AvailableTimes,
    ImmutableList<NotAvailableTime> NotAvailableTimes) : ILocationHoursOfOperationR6;

public record Location(HoursOfOperation? HoursOfOperation) : ILocation;
