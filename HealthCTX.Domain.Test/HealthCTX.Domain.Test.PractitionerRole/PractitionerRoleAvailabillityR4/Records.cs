using HealthCTX.Domain.Availability;
using HealthCTX.Domain.Period;
using HealthCTX.Domain.PractitionerRole;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleAvailabillityR4;

public record AvailableDaysOfWeek(string Value) : IAvailabilityAvailableDaysOfWeek;
public record AvailableAllDay(bool Value) : IAvailabilityAvailableAllDay;
public record AvailableStartTime(TimeOnly Value) : IAvailabilityAvailableStartTime;
public record AvailableEndTime(TimeOnly Value) : IAvailabilityAvailableEndTime;

public record AvailableTime(
    ImmutableList<AvailableDaysOfWeek> AvailableDays,
    AvailableAllDay? AllDay,
    AvailableStartTime? StartTime,
    AvailableEndTime? EndTime) : IPractitionerRoleAvailableTime;

public record NotAvailableDescription(string Value) : IAvailabilityNotAvailableDescription;
public record NotAvailableStartTime(DateTimeOffset Value) : IPeriodStart;
public record NotAvailableEndTime(DateTimeOffset Value) : IPeriodEnd;
public record NotAvailableDuring(NotAvailableStartTime StartTime, NotAvailableEndTime EndTime) : IAvailabilityNotAvailableDuring;

public record NotAvailableTime(NotAvailableDescription Description, NotAvailableDuring During) : IPractitionerRoleNotAvailable;

public record PractitionerRoleAvailabilityExceptions(string Value) : IPractitionerRoleAvailabilityExceptions;

public record PractitionerRole(
    ImmutableList<AvailableTime> Available,
    ImmutableList<NotAvailableTime> NotAvailable,
    PractitionerRoleAvailabilityExceptions? AvailabilityExceptions) : IPractitionerRole;
