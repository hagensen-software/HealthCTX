using HealthCTX.Domain.Organizations;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Organizations.OrganizationPartOf;

public record OrganizationPartOfReference(string Value) : IReferenceReference;
public record OrganizationPartOf(OrganizationPartOfReference Reference) : IOrganizationPartOf;

public record Organization(OrganizationPartOf PartOf) : IOrganization;
