using HealthCTX.Domain.Organizations;

namespace HealthCTX.Domain.Test.Organizations.OrganizationName;

public record OrganizationName(string Value) : IOrganizationName;
public record Organization(OrganizationName Name) : IOrganization;
