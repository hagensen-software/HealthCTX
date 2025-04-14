using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Organizations;

[FhirResource("Organization")]
[FhirProperty("identifier", typeof(IOrganizationIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IOrganizationActive), Cardinality.Optional)]
[FhirProperty("type", typeof(IOrganizationType), Cardinality.Multiple)]
[FhirProperty("name", typeof(IOrganizationName), Cardinality.Optional)]
[FhirProperty("alias", typeof(IOrganizationAlias), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IOrganizationTelecom), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("address", typeof(IOrganizationAddress), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("description", typeof(IOrganizationDescription), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("contact", typeof(IOrganizationContact), Cardinality.Multiple)]
[FhirProperty("partOf", typeof(IOrganizationPartOf), Cardinality.Optional)]
[FhirProperty("endpoint", typeof(IOrganizationEndpoint), Cardinality.Multiple)]
[FhirProperty("qualification", typeof(IOrganizationQualification), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
public interface IOrganization : IResource;
