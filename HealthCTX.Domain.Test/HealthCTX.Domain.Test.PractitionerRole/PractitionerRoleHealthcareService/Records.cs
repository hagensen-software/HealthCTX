using HealthCTX.Domain.PractitionerRole;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleHealthcareService;

public record HealthcareServiceReference(string Value) : IReferenceReference;
public record PractitionerRoleHealthcareService(HealthcareServiceReference Reference) : IPractitionerRoleHealthcareService;

public record PractitionerRole(ImmutableList<PractitionerRoleHealthcareService> HealthcareServices) : IPractitionerRole;
