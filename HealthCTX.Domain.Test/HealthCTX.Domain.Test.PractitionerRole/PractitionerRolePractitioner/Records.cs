using HealthCTX.Domain.PractitionerRole;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRolePractitioner;

public record PractitionerReference(string Value) : IReferenceReference;
public record PractitionerRolePractitioner(PractitionerReference Reference) : IPractitionerRolePractitioner;

public record PractitionerRole(PractitionerRolePractitioner Practitioner) : IPractitionerRole;
