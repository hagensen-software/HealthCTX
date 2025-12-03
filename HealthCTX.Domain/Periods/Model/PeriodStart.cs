namespace HealthCTX.Domain.Periods.Model;

/// <summary>
/// Represents the starting point of a time period using a specific date and time offset.
/// </summary>
public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
