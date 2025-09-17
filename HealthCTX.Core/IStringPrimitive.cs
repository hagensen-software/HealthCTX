using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR string primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IStringPrimitive : IElement
{
    /// <summary>
    /// Gets the string value associated with this instance.
    /// </summary>
    [FhirIgnore]
    string Value { get; init; }
}
