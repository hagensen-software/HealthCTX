using HealthCTX.Domain.Patients;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Patients.PatientManagingOrganization;

public record PatientManagingOrganizationReference(string Value) : IReferenceReference;
public record PatientManagingOrganization(PatientManagingOrganizationReference Reference) : IPatientManagingOrganization;

public record Patient(PatientManagingOrganization ManagingOrganization) : IPatient;