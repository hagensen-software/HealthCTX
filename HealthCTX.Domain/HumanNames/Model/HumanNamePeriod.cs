using HealthCTX.Domain.Periods.Model;

namespace HealthCTX.Domain.HumanNames.Model;

/// <summary>
/// Represents a time period during which a human name is or was valid, including optional start and end boundaries.
/// </summary>
public record HumanNamePeriod(
    PeriodStart? Start,
    PeriodEnd? End) : IHumanNamePeriod;
