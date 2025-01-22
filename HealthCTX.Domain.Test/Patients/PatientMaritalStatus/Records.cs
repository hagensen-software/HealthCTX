using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;

namespace HealthCTX.Domain.Test.Patients.PatientMaritalStatus;

public record PatientMaritalStatusCode(string Value) : ICodingCode;
public record PatientMaritalStatusSystem(Uri Value) : ICodingSystem;
public record PatientMaritalStatusDisplay(string Value) : ICodingDisplay;
public record PatientMaritalStatusCoding(
    PatientMaritalStatusCode Code,
    PatientMaritalStatusSystem System,
    PatientMaritalStatusDisplay Display) : ICodeableConceptCoding;

public record PatientMaritalStatusText(string Value) : ICodeableConceptText;

public record PatientMaritalStatus(PatientMaritalStatusCoding Coding, PatientMaritalStatusText Text) : IPatientMaritalStatus;

public record Patient(PatientMaritalStatus MaritalStatus) : IPatient;