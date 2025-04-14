using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IBooleanPrimitive : IElement
{
    [FhirIgnore]
    bool Value { get; init; }
}
