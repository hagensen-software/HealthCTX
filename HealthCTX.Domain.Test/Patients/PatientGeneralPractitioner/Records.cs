using HealthCTX.Domain.Patients;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Patients.PatientGeneralPractitioner;

public record GeneralPractitionerReference(string Value) : IReferenceReference;
public record PatientGeneralPractitioner(GeneralPractitionerReference Reference) : IPatientGeneralPractitioner;

public record Patient(PatientGeneralPractitioner GeneralPractitioner) : IPatient;