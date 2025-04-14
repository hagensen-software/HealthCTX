using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IStringPrimitive : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
