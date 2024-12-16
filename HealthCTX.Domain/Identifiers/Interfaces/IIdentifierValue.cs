using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Identifiers.Interfaces;

[FhirPrimitive]
public interface IIdentifierValue : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
