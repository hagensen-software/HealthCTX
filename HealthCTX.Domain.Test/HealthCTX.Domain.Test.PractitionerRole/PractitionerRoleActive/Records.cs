using HealthCTX.Domain.PractitionerRoles;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleActive;

public record PractitionerRoleActive(bool Value) : IPractitionerRoleActive;
public record PractitionerRole(PractitionerRoleActive Active) : IPractitionerRole;