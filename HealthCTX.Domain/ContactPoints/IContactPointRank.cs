using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.ContactPoints;

[FhirPrimitive]
public interface IContactPointRank : IElement
{
    [FhirIgnore]
    uint Value { get; init; }
}
