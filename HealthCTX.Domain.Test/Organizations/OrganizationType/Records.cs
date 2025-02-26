using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Organizations;

namespace HealthCTX.Domain.Test.Organizations.OrganizationType;

public record OrganizationTypeCode(string Value) : ICodingCode;
public record OrganizationTypeSystem(Uri Value) : ICodingSystem;
public record OrganizationTypeCoding(
    OrganizationTypeCode Code,
    OrganizationTypeSystem System) : ICodeableConceptCoding;

public record OrganizationType(OrganizationTypeCoding Coding) : IOrganizationType;

public record Organization(OrganizationType Type) : IOrganization;
