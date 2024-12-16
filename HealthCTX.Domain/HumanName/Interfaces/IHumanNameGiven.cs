using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.HumanName.Interfaces;

[FhirPrimitive]
public interface IHumanNameGiven : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
