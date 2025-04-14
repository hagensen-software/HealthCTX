using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Organizations;

[FhirElement]
[FhirProperty("identifier", typeof(IOrganizationQualificationIdentifier), Cardinality.Multiple)]
[FhirProperty("code", typeof(IOrganizationQualificationCode), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IOrganizationQualificationPeriod), Cardinality.Optional)]
[FhirProperty("issuer", typeof(IOrganizationQualificationIssuer), Cardinality.Optional)]
public interface IOrganizationQualification : IElement;
