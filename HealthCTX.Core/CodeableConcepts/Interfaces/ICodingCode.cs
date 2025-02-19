using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.CodeableConcepts.Interfaces;

[FhirPrimitive]
public interface ICodingCode : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
