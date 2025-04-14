using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IBase64BinaryPrimitive : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
