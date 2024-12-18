using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.References;

[FhirPrimitive]
public interface IReferenceDisplay
{
    [FhirIgnore]
    string Value { get; init; }
}
