using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IId : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
