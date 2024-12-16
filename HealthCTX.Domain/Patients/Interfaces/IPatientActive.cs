using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirPrimitive]
public interface IPatientActive : IElement
{
    [FhirIgnore]
    bool Value { get; init; }
}
