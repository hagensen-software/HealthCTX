using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for HL7 FHIR dateTime primitive.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IDateTimePrimitive : IElement
{
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
