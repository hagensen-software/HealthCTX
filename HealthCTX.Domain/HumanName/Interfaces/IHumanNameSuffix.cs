using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.HumanName.Interfaces;

[FhirPrimitive]
public interface IHumanNameSuffix : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
