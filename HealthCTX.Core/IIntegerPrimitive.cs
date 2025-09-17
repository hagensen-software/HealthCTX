using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR integer primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IIntegerPrimitive : IElement
{
    /// <summary>
    /// Gets the integer value associated with this instance.
    /// </summary>
    [FhirIgnore]
    int Value { get; init; }
}
