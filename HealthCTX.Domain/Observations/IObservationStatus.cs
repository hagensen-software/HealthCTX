using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation status.</para>
/// <para>The primitive element <see cref="ICodingCode"/> is supported.</para>
/// </summary>
public interface IObservationStatus : ICodingCode;
