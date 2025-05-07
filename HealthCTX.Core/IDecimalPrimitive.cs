using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IDecimalPrimitive : IElement
{
    [FhirIgnore]
    double Value { get; init; }
}
