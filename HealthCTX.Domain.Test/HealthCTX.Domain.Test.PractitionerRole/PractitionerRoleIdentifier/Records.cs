using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.PractitionerRole;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleIdentifier;

public record PractitionerRoleIdentifierSystem(Uri Value) : IIdentifierSystem;
public record PractitionerRoleIdentifierValue(string Value) : IIdentifierValue;
public record PractitionerRoleIdentifier(PractitionerRoleIdentifierSystem System, PractitionerRoleIdentifierValue Value) : IPractitionerRoleIdentifier;

public record PractitionerRole(PractitionerRoleIdentifier Identifier) : IPractitionerRole;