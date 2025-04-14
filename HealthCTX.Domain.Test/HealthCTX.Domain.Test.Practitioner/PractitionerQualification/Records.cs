using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Period;
using HealthCTX.Domain.Practitioner;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerQualification;

public record PractitionerIdentifierSystem(Uri Value) : IIdentifierSystem;
public record PractitionerIdentifierValue(string Value) : IIdentifierValue;
public record PractitionerQualificationIdentifier(PractitionerIdentifierSystem System, PractitionerIdentifierValue Value) : IPractitionerQualificationIdentifier;

public record PractitionerQualificationText(string Value) : ICodeableConceptText;
public record PractitionerQualificationCode(PractitionerQualificationText Text) : IPractitionerQualificationCodeableConcept;

public record PractitionerQualificationStart(DateTimeOffset Value) : IPeriodStart;
public record PractitionerQualificationPeriod(PractitionerQualificationStart Start) : IPractitionerQualificationPeriod;

public record PractitionerQualificationIssuerReference(string Value) : IReferenceReference;
public record PractitionerQualificationIssuer(PractitionerQualificationIssuerReference Reference) : IPractitionerQualificationIssuer;

public record PractitionerQualification(
    ImmutableList<PractitionerQualificationIdentifier> Identifiers,
    PractitionerQualificationCode Code,
    PractitionerQualificationPeriod? Period,
    PractitionerQualificationIssuer? Issuer) : IPractitionerQualification;

public record Practitioner(ImmutableList<PractitionerQualification> Qualifications) : IPractitioner;
