using HealthCTX.Domain.Period;
using HealthCTX.Domain.PractitionerRole;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRolePeriod;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record PractitionerRolePeriod(PeriodStart? Start, PeriodEnd? End) : IPractitionerRolePeriod;

public record PractitionerRole(PractitionerRolePeriod Period) : IPractitionerRole;