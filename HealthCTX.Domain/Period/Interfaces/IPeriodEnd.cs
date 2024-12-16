using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Period.Interfaces;

[FhirPrimitive]
public interface IPeriodEnd : IElement
{
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
