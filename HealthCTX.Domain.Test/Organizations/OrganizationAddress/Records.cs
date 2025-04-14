using HealthCTX.Domain.Address;
using HealthCTX.Domain.Organizations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Organizations.OrganizationAddress;

public record OrganizationAddressText(string Value) : IAddressText;
public record OrganizationAddress(OrganizationAddressText Text) : IOrganizationAddress;
public record Organization(ImmutableList<OrganizationAddress> Address) : IOrganization;
