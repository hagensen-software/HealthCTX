using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Organizations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Organizations.OrganizationQualificationStatusR6;

public record QualificationStatusCodingCode(string Value) : ICodingCode;
public record QualificationStatusCoding(QualificationStatusCodingCode Code) : ICodeableConceptCoding;
public record QualificationStatus(ImmutableList<QualificationStatusCoding> Codings) : IOrganizationQualificationStatus;

public record QualificationCodeCodingCode(string Value) : ICodingCode;
public record QualificationCodeCoding(QualificationCodeCodingCode Code) : ICodeableConceptCoding;
public record QualificationCode(ImmutableList<QualificationCodeCoding> Codings) : IOrganizationQualificationCode;

public record OrganizationQualification(
    QualificationCode Code,
    QualificationStatus? Status) : IOrganizationQualification;

public record Organization(ImmutableList<OrganizationQualification> Qualifications) : IOrganization;
