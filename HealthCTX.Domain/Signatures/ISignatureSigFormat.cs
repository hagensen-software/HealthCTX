using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Signatures;

/// <summary>
/// <para>Interface for HL7 FHIR Signature sigFormat.</para>
/// <para>The primitive element <see cref="ICodingCode"/> is supported.</para>
/// </summary>
public interface ISignatureSigFormat : ICodingCode;
