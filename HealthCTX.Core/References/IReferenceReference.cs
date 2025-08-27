using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.References;

/// <summary>
/// <para>Interface for HL7 FHIR Reference reference.</para>
/// <para>The Value property holds the .Net value of the primitive.</para>
/// </summary>
[FhirPrimitive]
public interface IReferenceReference : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
