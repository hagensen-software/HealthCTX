using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for HL7 FHIR Patient gender.</para>
/// <para>The primitive element <see cref="ICodingCode"/> is supported.</para>
/// </summary>
public interface IPatientGender : ICodingCode;