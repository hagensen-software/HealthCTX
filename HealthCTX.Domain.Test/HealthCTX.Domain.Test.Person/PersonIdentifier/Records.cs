using HealthCTX.Domain.Identifiers;

namespace HealthCTX.Domain.Test.Persons.PatientIdentifier;

public record PatientIdentifierSystem(Uri Value) : IIdentifierSystem;
public record PatientIdentifierValue(string Value) : IIdentifierValue;
public record PatientIdentifier(PatientIdentifierSystem System, PatientIdentifierValue Value) : IPatientIdentifier;

public record Patient(PatientIdentifier Identifier) : IPatient;