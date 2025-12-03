using HealthCTX.Domain.Periods.Model;

namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents a time period during which a contact point is or was in use.
/// </summary>
public record ContactPointPeriod(
    PeriodStart? Start,
    PeriodEnd? End
    ) : IContactPointPeriod;
