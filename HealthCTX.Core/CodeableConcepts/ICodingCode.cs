using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableConcepts;

/// <summary>
/// <para>Interface for HL7 FHIR code primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface ICodingCode : IElement
{
    /// <summary>
    /// Gets the code value of this instance as a <see cref="string"/>.
    /// </summary>
    [FhirIgnore]
    string Value { get; init; }
}
