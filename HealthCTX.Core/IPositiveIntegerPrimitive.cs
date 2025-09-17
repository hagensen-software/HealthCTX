using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR positiveInt primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IPositiveIntegerPrimitive : IElement
{
    /// <summary>
    /// Gets the unsignedInt value represented by this instance as an <see cref="uint"/>.
    /// </summary>
    [FhirIgnore]
    uint Value { get; init; }
}
