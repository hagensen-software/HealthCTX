using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR time primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface ITimePrimitive : IElement
{
    /// <summary>
    /// Gets the time value associated with this instance.
    /// </summary>
    [FhirIgnore]
    TimeOnly Value { get; init; }
}
