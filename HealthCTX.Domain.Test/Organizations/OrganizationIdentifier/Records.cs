using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Organizations;

namespace HealthCTX.Domain.Test.Organizations.OrganizationIdentifier;

public record OrganizationIdentifierSystem(Uri Value) : IIdentifierSystem;
public record OrganizationIdentifierValue(string Value) : IIdentifierValue;
public record OrganizationIdentifier(OrganizationIdentifierSystem System, OrganizationIdentifierValue Value) : IOrganizationIdentifier;

public record Organization(OrganizationIdentifier Identifier) : IOrganization;
