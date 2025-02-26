using HealthCTX.Domain.Organizations;

namespace HealthCTX.Domain.Test.Organizations.OrganizationActive;

public record OrganizationActive(bool Value) : IOrganizationActive;
public record Organization(OrganizationActive Active) : IOrganization;