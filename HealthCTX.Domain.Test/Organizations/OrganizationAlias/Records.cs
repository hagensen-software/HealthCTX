using HealthCTX.Domain.Organizations;

namespace HealthCTX.Domain.Test.Organizations.OrganizationAlias;

public record OrganizationAlias(string Value) : IOrganizationAlias;
public record Organization(OrganizationAlias Alias) : IOrganization;
