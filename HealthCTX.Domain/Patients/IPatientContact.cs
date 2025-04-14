using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Patients;

[FhirElement]
[FhirProperty("relationship", typeof(IPatientContactRelationship), Cardinality.Multiple)]
[FhirProperty("name", typeof(IPatientContactHumanName), Cardinality.Optional)]
[FhirProperty("telecom", typeof(IPatientContactContactPoint), Cardinality.Multiple)]
[FhirProperty("address", typeof(IPatientContactAddress), Cardinality.Optional)]
[FhirProperty("gender", typeof(IPatientContactGender), Cardinality.Optional)]
[FhirProperty("organization", typeof(IPatientContactOrganization), Cardinality.Optional)]
[FhirProperty("period", typeof(IPatientContactPeriod), Cardinality.Optional)]
public interface IPatientContact : IElement;
