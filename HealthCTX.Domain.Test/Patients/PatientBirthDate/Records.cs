using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Patients.PatientBirthDate;

public record PatientBirthDate(DateOnly Value) : IPatientBirthDate;

public record Patient(PatientBirthDate BirthDate) : IPatient;
