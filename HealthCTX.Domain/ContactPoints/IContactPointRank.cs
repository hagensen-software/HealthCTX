using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.ContactPoints;

[FhirPrimitive]
public interface IContactPointRank : IElement
{
    [FhirIgnore]
    uint Value { get; init; }
}
