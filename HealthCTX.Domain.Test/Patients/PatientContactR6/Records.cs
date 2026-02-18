using HealthCTX.Domain.Addresses;
using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Patients;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Patients.PatientContactR6;

public record ContactRoleCodingCode(string Value) : ICodingCode;
public record ContactRoleCoding(ContactRoleCodingCode Code) : ICodeableConceptCoding;
public record ContactRole(ImmutableList<ContactRoleCoding> Codings) : IPatientContactRole;

public record AdditionalNameFamily(string Value) : IHumanNameFamily;
public record AdditionalNameGiven(string Value) : IHumanNameGiven;
public record AdditionalName(
    AdditionalNameFamily? Family,
    ImmutableList<AdditionalNameGiven> Given) : IPatientContactAdditionalName;

public record AdditionalAddressCity(string Value) : IAddressCity;
public record AdditionalAddressCountry(string Value) : IAddressCountry;
public record AdditionalAddress(
    AdditionalAddressCity? City,
    AdditionalAddressCountry? Country) : IPatientContactAdditionalAddress;

public record PatientContact(
    ImmutableList<ContactRole> Roles,
    ImmutableList<AdditionalName> AdditionalNames,
    ImmutableList<AdditionalAddress> AdditionalAddresses) : IPatientContact;

public record Patient(ImmutableList<PatientContact> Contacts) : IPatient;
