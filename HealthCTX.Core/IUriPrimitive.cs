using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IUriPrimitive : IElement
{
    [FhirIgnore]
    Uri Value { get; init; }
}
