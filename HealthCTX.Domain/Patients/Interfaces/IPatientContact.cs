using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirElement]
[FhirProperty("relationship", typeof(IPatientContactRelationship), Cardinality.Multiple)]
[FhirProperty("name", typeof(IPatientContactHumanName), Cardinality.Optional)]
[FhirProperty("telecom", typeof(IPatientContactContactPoint), Cardinality.Multiple)]
[FhirProperty("address", typeof(IPatientContactAddress), Cardinality.Optional)]
[FhirProperty("gender", typeof(IPatientContactGender), Cardinality.Optional)]
[FhirProperty("organization", typeof(IPatientContactOrganization), Cardinality.Optional)]
[FhirProperty("period", typeof(IPatientContactPeriod), Cardinality.Optional)]
public interface IPatientContact : IElement;
