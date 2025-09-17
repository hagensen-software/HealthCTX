using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR boolean primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IBooleanPrimitive : IElement
{
    /// <summary>
    /// Gets the boolean value associated with this instance.
    /// </summary>
    [FhirIgnore]
    bool Value { get; init; }
}
