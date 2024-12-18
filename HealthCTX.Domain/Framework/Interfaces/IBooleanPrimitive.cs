using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IBooleanPrimitive : IElement
{
    [FhirIgnore]
    bool Value { get; init; }
}
