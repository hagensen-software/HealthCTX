using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IBase64BinaryPrimitive : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
