using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR instant primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IInstantPrimitive : IElement
{
    /// <summary>
    /// Gets the instant value assouciated with this instance as a <see cref="DateTimeOffset"/>.
    /// </summary>
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
