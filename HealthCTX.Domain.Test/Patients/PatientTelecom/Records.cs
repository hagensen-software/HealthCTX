using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.Patients.Interfaces;

namespace HealthCTX.Domain.Test.Patients.PatientTelecom;

public record PatientTelecomValue(string Value) : IContactPointValue;
public record PatientTelecomSystem(string Value) : IContactPointSystem;
public record PatientTelecom(PatientTelecomSystem System, PatientTelecomValue Value) : IPatientContactPoint;
public record Patient(PatientTelecom Telecom) : IPatient;