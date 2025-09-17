using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Patients;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Patients.PatientName;

public record PatientFamilyName(string Value) : IHumanNameFamily;
public record PatientGivenName(string Value) : IHumanNameGiven;

public record PatientName(PatientFamilyName Family, ImmutableList<PatientGivenName> Given) : IPatientHumanName;

public record Patient(PatientName Name) : IPatient;