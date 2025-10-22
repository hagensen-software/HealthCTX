using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableConcepts;

/// <summary>
/// <para>Interface for HL7 FHIR code primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
public interface ICodingCode : IStringPrimitive;
