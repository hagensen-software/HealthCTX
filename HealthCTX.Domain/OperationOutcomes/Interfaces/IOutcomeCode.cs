using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes.Interfaces;

[FhirPrimitive]
public interface IOutcomeCode : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
