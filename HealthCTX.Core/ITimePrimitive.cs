using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface ITimePrimitive : IElement
{
    [FhirIgnore]
    TimeOnly Value { get; init; }
}
