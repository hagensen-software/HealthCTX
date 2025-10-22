using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR oid primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IOidPrimitive : IElement
{
    /// <summary>
    /// Gets the url value of this instance as a <see cref="Uri"/>.
    /// </summary>
    [FhirIgnore]
    Uri Value { get; init; }
}
