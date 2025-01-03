using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirElement]
[FhirProperty("relationship", typeof(IPatientContactRelationship), Cardinality.Multiple)]
[FhirProperty("name", typeof(IPatientContactHumanName), Cardinality.Single)]
[FhirProperty("telecom", typeof(IPatientContactContactPoint), Cardinality.Multiple)]
[FhirProperty("address", typeof(IPatientContactAddress), Cardinality.Single)]
[FhirProperty("gender", typeof(IPatientContactGender), Cardinality.Single)]
[FhirProperty("organization", typeof(IPatientContactOrganization), Cardinality.Single)]
[FhirProperty("period", typeof(IPatientContactPeriod), Cardinality.Single)]
public interface IPatientContact : IElement;
