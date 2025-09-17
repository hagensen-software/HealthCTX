using HealthCTX.Domain.PractitionerRoles;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleLocation;

public record LocationReference(string Value) : IReferenceReference;
public record PractitionerRoleLocation(LocationReference Reference) : IPractitionerRoleLocation;

public record PractitionerRole(ImmutableList<PractitionerRoleLocation> Locations) : IPractitionerRole;
