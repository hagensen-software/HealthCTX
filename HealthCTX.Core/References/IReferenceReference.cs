using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.References;

[FhirPrimitive]
public interface IReferenceReference : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
