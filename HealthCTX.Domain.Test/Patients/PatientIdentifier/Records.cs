using HealthCTX.Domain.Identifiers.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;

namespace HealthCTX.Domain.Test.Patients.PatientIdentifier;

public record PatientIdentifierSystem(Uri Value) : IIdentifierSystem;
public record PatientIdentifierValue(string Value) : IIdentifierValue;
public record PatientIdentifier(PatientIdentifierSystem System, PatientIdentifierValue Value) : IPatientIdentifier;

public record Patient(PatientIdentifier Identifier) : IPatient;