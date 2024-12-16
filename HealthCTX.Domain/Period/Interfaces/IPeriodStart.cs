using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Period.Interfaces;

[FhirPrimitive]
public interface IPeriodStart : IElement
{
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
