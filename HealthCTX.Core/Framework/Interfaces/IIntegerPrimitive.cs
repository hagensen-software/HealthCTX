using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IIntegerPrimitive : IElement
{
    [FhirIgnore]
    int Value { get; init; }
}
