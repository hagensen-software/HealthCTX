using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Patients;

[FhirElement]
[FhirProperty("other", typeof(IPatientLinkOther), Cardinality.Mandatory)]
[FhirProperty("type", typeof(IPatientLinkType), Cardinality.Mandatory)]
public interface IPatientLink : IElement;
