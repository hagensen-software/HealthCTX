using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IInteger64Primitive : IElement
{
    [FhirIgnore]
    long Value { get; init; }
}
