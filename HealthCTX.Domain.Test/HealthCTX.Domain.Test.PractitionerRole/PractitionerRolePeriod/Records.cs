using HealthCTX.Domain.Periods;
using HealthCTX.Domain.PractitionerRoles;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRolePeriod;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record PractitionerRolePeriod(PeriodStart? Start, PeriodEnd? End) : IPractitionerRolePeriod;

public record PractitionerRole(PractitionerRolePeriod Period) : IPractitionerRole;