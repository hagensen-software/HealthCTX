using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.CodeableConcepts.Interfaces;

[FhirPrimitive]
public interface ICodingDisplay : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
