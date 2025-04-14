using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IDatePrimitive : IElement
{
    [FhirIgnore]
    DateOnly Value { get; init; }
}
