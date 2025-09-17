using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR date primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IDatePrimitive : IElement
{
    /// <summary>
    /// Gets the date value associated with this instance.
    /// </summary>
    [FhirIgnore]
    DateOnly Value { get; init; }
}
