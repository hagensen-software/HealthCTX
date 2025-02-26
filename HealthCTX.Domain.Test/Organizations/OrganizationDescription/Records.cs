using HealthCTX.Domain.Organizations;

namespace HealthCTX.Domain.Test.Organizations.OrganizationDescription;

public record OrganizationDescription(string Value) : IOrganizationDescription;
public record Organization(OrganizationDescription Description) : IOrganization;
