using HealthCTX.Domain.Availability;
using HealthCTX.Domain.Period;
using HealthCTX.Domain.PractitionerRole;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleAvailability;

public record AvailableDaysOfWeek(string Value) : IAvailabilityAvailableDaysOfWeek;
public record AvailableAllDay(bool Value) : IAvailabilityAvailableAllDay;
public record AvailableStartTime(TimeOnly Value) : IAvailabilityAvailableStartTime;
public record AvailableEndTime(TimeOnly Value) : IAvailabilityAvailableEndTime;

public record AvailableTime(
    ImmutableList<AvailableDaysOfWeek> AvailableDays,
    AvailableAllDay? AllDay,
    AvailableStartTime? StartTime,
    AvailableEndTime? EndTime) : IAvailabilityAvailable;

public record NotAvailableDescription(string Value) : IAvailabilityNotAvailableDescription;
public record NotAvailableStartTime(DateTimeOffset Value) : IPeriodStart;
public record NotAvailableEndTime(DateTimeOffset Value) : IPeriodEnd;
public record NotAvailableDuring(NotAvailableStartTime StartTime, NotAvailableEndTime EndTime) : IAvailabilityNotAvailableDuring;

public record NotAvailableTime(NotAvailableDescription Description, NotAvailableDuring During) : IAvailabilityNotAvailable;

public record PractitionerRoleAvailability(AvailableTime? Available, NotAvailableTime? NotAvailable) : IPractitionerRoleAvailability;

public record PractitionerRole(ImmutableList<PractitionerRoleAvailability> Availability) : IPractitionerRole;