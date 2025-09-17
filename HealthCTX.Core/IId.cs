using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR integer64 primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IId : IElement
{
    /// <summary>
    /// Gets the id value of the FHIR element as a <see cref="string"/>.
    /// </summary>
    [FhirIgnore]
    string Value { get; init; }
}
