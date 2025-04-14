using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Patients.PatientDeceasedDateTime;

public record PatientDeceasedDateTime(DateTimeOffset Value) : IPatientDeceasedDateTime;

public record Patient(PatientDeceasedDateTime Deceased) : IPatient;
