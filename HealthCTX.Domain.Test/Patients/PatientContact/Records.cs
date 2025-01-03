using HealthCTX.Domain.Address.Interfaces;
using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.HumanName.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;
using HealthCTX.Domain.Period.Interfaces;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Patients.PatientContact;

public record RelationshipSystem(Uri Value) : ICodingSystem;
public record RelationshipCode(string Value) : ICodingCode;
public record RelationshipCoding(RelationshipSystem System, RelationshipCode Code) : ICodeableConceptCoding;
public record RelationshipText(string Value) : ICodeableConceptText;
public record PatientContactRelationship(RelationshipCoding Coding, RelationshipText Text) : IPatientContactRelationship;

public record FamilyName(string Value) : IHumanNameFamily;
public record GivenName(string Value) : IHumanNameGiven;
public record PatientContactHumanName(FamilyName FamilyName, GivenName GivenName) : IPatientContactHumanName;

public record ContactPointSystem(string Value) : IContactPointSystem;
public record ContactPointValue(string Value) : IContactPointValue;
public record PatientContactContactPoint(
    ContactPointSystem System,
    ContactPointValue Value) : IPatientContactContactPoint;

public record PatientContactLine(string Value) : IAddressLine;
public record PatientContactCity(string Value) : IAddressCity;
public record PatientContactPostalCode(string Value) : IAddressPostalCode;
public record PatientContactAddress(
    ImmutableList<PatientContactLine> Line,
    PatientContactCity City,
    PatientContactPostalCode PostalCode) : IPatientContactAddress;

public record PatientContactGender(string Value) : IPatientContactGender;
public record PatientContactReference(string Value) : IReferenceReference;
public record PatientContactOrganization(PatientContactReference Reference) : IPatientContactOrganization;
public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record PatientContactPeriod(PeriodStart Start, PeriodEnd End) : IPatientContactPeriod;

public record PatientContact(
    PatientContactRelationship Relationship,
    PatientContactHumanName Name,
    ImmutableList<PatientContactContactPoint> Telecom,
    PatientContactAddress Address,
    PatientContactGender Gender,
    PatientContactOrganization Organization,
    PatientContactPeriod Period) : IPatientContact;

public record Patient(ImmutableList<PatientContact> Contacts) : IPatient;