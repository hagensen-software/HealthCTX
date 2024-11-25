using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.CodeableConcepts.Interfaces;

[FhirPrimitive]
public interface IUserSelectedBoolean : IElement
{
    [FhirIgnore]
    bool Value { get; init; }
}
