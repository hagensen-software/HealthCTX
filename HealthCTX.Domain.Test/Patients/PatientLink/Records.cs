using HealthCTX.Domain.Patients.Interfaces;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Patients.PatientLink;

public record PatientLinkOtherReference(string Value) : IReferenceReference;
public record PatientLinkOther(PatientLinkOtherReference Reference) : IPatientLinkOther;

public record PatientLinkType(string Value) : IPatientLinkType;

public record PatientLink(PatientLinkOther Other, PatientLinkType Type) : IPatientLink;

public record Patient(PatientLink Link) : IPatient;
