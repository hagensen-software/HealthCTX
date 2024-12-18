using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IDatePrimitive : IElement
{
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
