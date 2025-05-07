using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface ICanonicalPrimitive : IElement
{
    [FhirIgnore]
    Uri Value { get; init; }
}
