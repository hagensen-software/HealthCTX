using HealthCTX.Domain.PractitionerRole;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleActive;

public record PractitionerRoleActive(bool Value) : IPractitionerRoleActive;
public record PractitionerRole(PractitionerRoleActive Active) : IPractitionerRole;