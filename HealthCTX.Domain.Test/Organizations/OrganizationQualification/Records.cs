using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Organizations;
using HealthCTX.Domain.Period;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Organizations.OrganizationQualification;

public record OrganizationQualificationIdentifierSystem(Uri Value) : IIdentifierSystem;
public record OrganizationQualificationIdentifierValue(string Value) : IIdentifierValue;
public record OrganizationQualificationIdentifier(
    OrganizationQualificationIdentifierSystem System,
    OrganizationQualificationIdentifierValue Value) : IOrganizationQualificationIdentifier;

public record OrganizationQualificationCodeText(string Value) : ICodeableConceptText;
public record OrganizationQualificationCode(OrganizationQualificationCodeText Text) : IOrganizationQualificationCode;

public record OrganizationQualificationPeriodStart(DateTimeOffset Value) : IPeriodStart;
public record OrganizationQualificationPeriod(OrganizationQualificationPeriodStart Start) : IOrganizationQualificationPeriod;

public record OrganizationQualificationIssuerReference(string Value) : IReferenceReference;
public record OrganizationQualificationIssuer(OrganizationQualificationIssuerReference Reference) : IOrganizationQualificationIssuer;

public record OrganizationQualification(
    ImmutableList<OrganizationQualificationIdentifier> Identifier,
    OrganizationQualificationCode Code,
    OrganizationQualificationPeriod Period,
    OrganizationQualificationIssuer Issuer) : IOrganizationQualification;

public record Organization(ImmutableList<OrganizationQualification> Qualifications) : IOrganization;
