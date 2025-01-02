using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IDateTimePrimitive : IElement
{
    [FhirIgnore]
    DateTimeOffset Value { get; init; }
}
