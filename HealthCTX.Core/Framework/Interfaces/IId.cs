using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IId : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
