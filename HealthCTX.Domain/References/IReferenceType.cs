using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.References;

[FhirPrimitive]
public interface IReferenceType : IElement
{
    [FhirIgnore]
    Uri Value { get; init; }
}
