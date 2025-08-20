using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Organizations;

/// <summary>
/// <para>Interface for the HL7 FHIR Organization resource.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>identifier</term>
///         <description><see cref="IOrganizationIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>active</term>
///         <description><see cref="IOrganizationActive"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IOrganizationType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>name</term>
///         <description><see cref="IOrganizationName"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>alias</term>
///         <description><see cref="IOrganizationAlias"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>telecom</term>
///         <description><see cref="IOrganizationTelecom"/> (HL7 FHIR R4 only)</description>
///     </item>
///     <item>
///         <term>address</term>
///         <description><see cref="IOrganizationAddress"/> (HL7 FHIR R4 only)</description>
///     </item>
///     <item>
///         <term>description</term>
///         <description><see cref="IOrganizationDescription"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>contact</term>
///         <description><see cref="IOrganizationContact"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>partOf</term>
///         <description><see cref="IOrganizationPartOf"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>endpoint</term>
///         <description><see cref="IOrganizationEndpoint"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>qualification</term>
///         <description><see cref="IOrganizationQualification"/> (HL7 FHIR R5 only)</description>
///     </item>
/// </list>
/// </summary>
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
