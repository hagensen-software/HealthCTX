using HealthCTX.Domain.Organizations;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Organizations.OrganizationEndpoint;

public record OrganizationEndpointReference(string Value) : IReferenceReference;
public record OrganizationEndpoint(OrganizationEndpointReference Reference) : IOrganizationEndpoint;

public record Organization(OrganizationEndpoint Endpoint) : IOrganization;
