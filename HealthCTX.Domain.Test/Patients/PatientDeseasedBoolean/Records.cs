using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Patients.PatientDeseasedBoolean;

public record PatientDeceasedBoolean(bool Value) : IPatientDeceasedBoolean;

public record Patient(PatientDeceasedBoolean Deceased) : IPatient;
