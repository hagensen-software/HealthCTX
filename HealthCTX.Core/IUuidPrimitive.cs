using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR uuid primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IUuidPrimitive : IElement
{
    /// <summary>
    /// Gets the url value of this instance as a <see cref="Uri"/>.
    /// </summary>
    [FhirIgnore]
    Uri Value { get; init; }
}
