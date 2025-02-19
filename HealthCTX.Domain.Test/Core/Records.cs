using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Framework.Interfaces;
using HealthCTX.Domain.Identifiers.Interfaces;
using HealthCTX.Domain.Organizations;
using HealthCTX.Domain.Patients.Interfaces;
using HealthCTX.Domain.Period.Interfaces;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test;

public record PatientId(string Value) : IId;

public record IdentifierUse(string Value) : IIdentifierUse;
public record IdentifierCode(string Value) : ICodingCode;
public record IdentifierCoding(IdentifierCode Code) : ICodeableConceptCoding;
public record IdentifierText(string Value) : ICodeableConceptText;
public record IdentifierType(IdentifierCoding Coding, IdentifierText Text) : IIdentifierType;
public record IdentifierSystem(Uri Value) : IIdentifierSystem;

public record IdentifierValue(string Value) : IIdentifierValue;
public record IdentifierOrganizationReference(string Value) : IReferenceReference;
public record IdentifierAssigner(IdentifierOrganizationReference OrganizationReference) : IIdentifierAssigner;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record IdentifierPeriod(
    PeriodStart PeriodStart,
    PeriodEnd PeriodEnd) : IIdentifierPeriod;

public record PatientIdentifier(
    IdentifierUse Use,
    IdentifierType Type,
    IdentifierSystem System,
    IdentifierValue Value,
    IdentifierPeriod Period,
    IdentifierAssigner Assigner
    ) : IPatientIdentifier;

public record MaritalStatusSystem(Uri Value) : ICodingSystem;
public record MaritalStatusVersion(string Value) : ICodingVersion;
public record MaritalStatusCode(string Value) : ICodingCode;
public record MaritalStatusDisplay(string Value) : ICodingDisplay;
public record MaritalStatusUserSelected(bool Value) : ICodingUserSelected;
public record MaritalStatusCoding(
    MaritalStatusSystem System,
    MaritalStatusVersion Version,
    MaritalStatusCode Code,
    MaritalStatusDisplay Display,
    MaritalStatusUserSelected? UserSelected) : ICodeableConceptCoding;
public record MaritalStatusText(string Value) : ICodeableConceptText;

public record MaritalStatus(ImmutableList<MaritalStatusCoding> Coding, MaritalStatusText Text) : IPatientMaritalStatus;

public record Patient(PatientId PatientId, ImmutableList<PatientIdentifier> Identifiers, MaritalStatus? MaritalStatus) : IPatient;

public record OrganizationId(string Value) : IId;
public record PatientOrganization(OrganizationId Id) : IOrganization;
