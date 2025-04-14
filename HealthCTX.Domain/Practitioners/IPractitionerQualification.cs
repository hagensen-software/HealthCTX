using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Practitioners;

[FhirElement]
[FhirProperty("identifier", typeof(IPractitionerQualificationIdentifier), Cardinality.Multiple)]
[FhirProperty("code", typeof(IPractitionerQualificationCodeableConcept), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IPractitionerQualificationPeriod), Cardinality.Optional)]
[FhirProperty("issuer", typeof(IPractitionerQualificationIssuer), Cardinality.Optional)]
public interface IPractitionerQualification : IElement;
