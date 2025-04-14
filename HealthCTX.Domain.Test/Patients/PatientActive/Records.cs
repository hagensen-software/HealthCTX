using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Patients.PatientActive;

public record PatientActive(bool Value) : IPatientActive;
public record Patient(PatientActive Active) : IPatient;