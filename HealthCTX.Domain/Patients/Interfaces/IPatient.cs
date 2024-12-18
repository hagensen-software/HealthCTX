using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirResource("Patient")]
[FhirProperty("identifier", typeof(IPatientIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPatientActive), Cardinality.Single)]
[FhirProperty("name", typeof(IPatientHumanName), Cardinality.Multiple)]
[FhirProperty("maritalStatus", typeof(IMaritalStatusCodeableConcept), Cardinality.Single)]
public interface IPatient : IResource;
