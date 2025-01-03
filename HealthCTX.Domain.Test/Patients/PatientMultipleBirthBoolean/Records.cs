using HealthCTX.Domain.Patients.Interfaces;

namespace HealthCTX.Domain.Test.Patients.PatientMultipleBirthBoolean;

public record PatientMultipleBirthBoolean(bool Value) : IPatientMultipleBirthBoolean;

public record Patient(PatientMultipleBirthBoolean MultipleBirth) : IPatient;
