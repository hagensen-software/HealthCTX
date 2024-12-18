using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IStringPrimitive : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
