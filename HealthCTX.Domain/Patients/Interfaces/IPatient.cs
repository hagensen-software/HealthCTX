using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirResource("Patient")]
[FhirProperty("maritalStatus", typeof(IMaritalStatusCodeableConcept))]
public interface IPatient : IResource
{
}
