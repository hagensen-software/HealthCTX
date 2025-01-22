using HealthCTX.Domain.Framework.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;

namespace HealthCTX.Domain.Test.Patients.PatientId;

public record PatientId(string Value) : IId;

public record Patient(PatientId Id) : IPatient;
