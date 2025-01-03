using HealthCTX.Domain.Patients.Interfaces;

namespace HealthCTX.Domain.Test.Patients.PatientMultipleBirthInteger;

public record PatientMultipleBirthInteger(int Value) : IPatientMultipleBirthInteger;

public record Patient(PatientMultipleBirthInteger MultipleBirthNumber) : IPatient;
