using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.References;

[FhirPrimitive]
public interface IReferenceReference : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
