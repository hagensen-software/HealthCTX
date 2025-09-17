using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR canonical primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface ICanonicalPrimitive : IElement
{
    /// <summary>
    /// Gets the canonical value associated with this instance as a <see cref="Uri"/>.
    /// </summary>
    [FhirIgnore]
    Uri Value { get; init; }
}
