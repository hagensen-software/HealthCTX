using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.PractitionerRole;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleCode;

public record RoleCode(string Value) : ICodingCode;
public record RoleSystem(Uri Value) : ICodingSystem;
public record PractitionerRoleCoding(
    RoleCode Code,
    RoleSystem System) : ICodeableConceptCoding;

public record PractitionerRoleCode(PractitionerRoleCoding Coding) : IPractitionerRoleCode;

public record PractitionerRole(ImmutableList<PractitionerRoleCode> Codes) : IPractitionerRole;
