namespace HealthCTX.Domain.Periods.Model;

/// <summary>
/// Represents the end point of a time period, encapsulated as a date and time value.
/// </summary>
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
