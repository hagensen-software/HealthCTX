using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR integer64 primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IInteger64Primitive : IElement
{
    /// <summary>
    /// Gets the value associated with this instance as a <see cref="long"/>.
    /// </summary>
    [FhirIgnore]
    long Value { get; init; }
}
