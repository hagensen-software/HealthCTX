using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Patients.PatientIdentifier;

public record PatientIdentifierSystem(Uri Value) : IIdentifierSystem;
public record PatientIdentifierValue(string Value) : IIdentifierValue;
public record PatientIdentifier(PatientIdentifierSystem System, PatientIdentifierValue Value) : IPatientIdentifier;

public record Patient(PatientIdentifier Identifier) : IPatient;