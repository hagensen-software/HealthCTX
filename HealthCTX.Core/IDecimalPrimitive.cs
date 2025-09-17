using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR decimal primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IDecimalPrimitive : IElement
{
    /// <summary>
    /// Gets the decimal value associated with this instance as a <see cref="double"/>.
    /// </summary>
    [FhirIgnore]
    double Value { get; init; }
}
