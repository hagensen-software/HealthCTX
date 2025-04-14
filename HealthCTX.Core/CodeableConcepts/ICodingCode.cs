using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableConcepts;

[FhirPrimitive]
public interface ICodingCode : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
