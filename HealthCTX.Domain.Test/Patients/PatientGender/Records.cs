using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Patients.PatientGender;

public record PatientGender(string Value) : IPatientGender;

public record Patient(PatientGender Gender) : IPatient;