using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Patients.PatientId;

public record PatientId(string Value) : IId;

public record Patient(PatientId Id) : IPatient;
