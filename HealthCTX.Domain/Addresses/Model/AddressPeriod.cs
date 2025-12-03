
using HealthCTX.Domain.Periods.Model;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a period during which an address is valid, defined by optional start and end boundaries.
/// </summary>
public record AddressPeriod(
    PeriodStart? Start,
    PeriodEnd? End
    ) : IAddressPeriod;
