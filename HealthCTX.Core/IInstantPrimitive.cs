using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IInstantPrimitive : IElement
{
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
