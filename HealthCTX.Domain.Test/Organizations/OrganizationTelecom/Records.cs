using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.Organizations;

namespace HealthCTX.Domain.Test.Organizations.OrganizationTelecom;

public record OrganizationTelecomValue(string Value) : IContactPointValue;
public record OrganizationTelecomSystem(string Value) : IContactPointSystem;
public record OrganizationTelecom(OrganizationTelecomSystem System, OrganizationTelecomValue Value) : IOrganizationTelecom;
public record Organization(OrganizationTelecom Telecom) : IOrganization;
