using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR markdown primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IMarkdownPrimitive : IElement
{
    /// <summary>
    /// Gets the markdown value of the FHIR element as a <see cref="string"/>.
    /// </summary>
    [FhirIgnore]
    string Value { get; init; }
}
