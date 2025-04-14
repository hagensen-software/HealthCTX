using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IIntegerPrimitive : IElement
{
    [FhirIgnore]
    int Value { get; init; }
}
