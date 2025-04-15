using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.PractitionerRole;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleSpecialty;

public record SpecialtyCode(string Value) : ICodingCode;
public record SpecialtySystem(Uri Value) : ICodingSystem;
public record PractitionerRoleSpecialtyCoding(
    SpecialtyCode Code,
    SpecialtySystem System) : ICodeableConceptCoding;

public record PractitionerRoleSpecialty(PractitionerRoleSpecialtyCoding Coding) : IPractitionerRoleSpecialty;

public record PractitionerRole(ImmutableList<PractitionerRoleSpecialty> Specialties) : IPractitionerRole;
