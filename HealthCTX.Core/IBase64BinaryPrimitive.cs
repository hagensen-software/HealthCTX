using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR base64Binary primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IBase64BinaryPrimitive : IElement
{
    /// <summary>
    /// Gets the base64Binary value associated with this instance as a <see cref="string"/>.
    /// </summary>
    [FhirIgnore]
    string Value { get; init; }
}
