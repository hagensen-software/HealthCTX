using HealthCTX.Domain.Periods.Model;

namespace HealthCTX.Domain.Identifiers.Model;

/// <summary>
/// Represents a period defined by a start and end date and time.
/// </summary>
public record IdentifierPeriod(
    PeriodStart? Start,
    PeriodEnd? End) : IIdentifierPeriod;
