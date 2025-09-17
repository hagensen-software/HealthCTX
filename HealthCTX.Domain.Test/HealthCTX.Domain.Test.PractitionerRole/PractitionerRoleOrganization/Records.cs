using HealthCTX.Domain.PractitionerRoles;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleOrganization;

public record OrganizationReference(string Value) : IReferenceReference;
public record PractitionerRoleOrganization(OrganizationReference Reference) : IPractitionerRoleOrganization;

public record PractitionerRole(PractitionerRoleOrganization Organization) : IPractitionerRole;
