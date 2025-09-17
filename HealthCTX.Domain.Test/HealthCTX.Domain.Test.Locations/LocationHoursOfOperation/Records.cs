using HealthCTX.Domain.Availabilities;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationHoursOfOperation;

public record OperationDaysOfWeek(string Value) : IAvailabilityAvailableDaysOfWeek;
public record OperationAllDay(bool Value) : IAvailabilityAvailableAllDay;
public record OperationOpeningTime(TimeOnly Value) : ILocationHoursOfOperationOpeningTime;
public record OperationClosingTime(TimeOnly Value) : ILocationHoursOfOperationClosingTime;

public record LocationHoursOfOperationR4(
    ImmutableList<OperationDaysOfWeek> DaysOfWeek,
    OperationAllDay? AllDay,
    OperationOpeningTime OpeningTime,
    OperationClosingTime ClosingTime) : ILocationHoursOfOperation;
public record LocationR4(
    ImmutableList<LocationHoursOfOperationR4> HoursOfOperation) : ILocation;


public record AvailableStartTime(TimeOnly Value) : IAvailabilityAvailableStartTime;
public record LocationAvailable(AvailableStartTime AvailableStartTime) : IAvailabilityAvailable;

public record NotAvailableDescription(string Value) : IAvailabilityNotAvailableDescription;
public record LocationNotAvailable(NotAvailableDescription Description) : IAvailabilityNotAvailable;

public record LocationHoursOfOperationR5(LocationAvailable Available, LocationNotAvailable NotAvailable) : ILocationHoursOfOperation;
public record LocationR5(
    ImmutableList<LocationHoursOfOperationR5> HoursOfOperation) : ILocation;
