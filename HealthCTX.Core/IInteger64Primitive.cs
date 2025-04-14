using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IInteger64Primitive : IElement
{
    [FhirIgnore]
    long Value { get; init; }
}
