using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IDateTimePrimitive : IElement
{
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
