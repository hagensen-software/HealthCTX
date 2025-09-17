using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR uri primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IUriPrimitive : IElement
{
    /// <summary>
    /// Gets the uri value represented by this instance.
    /// </summary>
    [FhirIgnore]
    Uri Value { get; init; }
}
