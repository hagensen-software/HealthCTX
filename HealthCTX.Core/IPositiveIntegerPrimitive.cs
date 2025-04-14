using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IPositiveIntegerPrimitive : IElement
{
    [FhirIgnore]
    uint Value { get; init; }
}
