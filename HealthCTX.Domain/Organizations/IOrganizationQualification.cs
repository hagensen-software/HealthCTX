using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Organizations;

/// <summary>
/// <para>Interface for the HL7 FHIR Organization qualification element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>identifier</term>
///         <description><see cref="IOrganizationQualificationIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>code</term>
///         <description><see cref="IOrganizationQualificationCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IOrganizationQualificationPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>issuer</term>
///         <description><see cref="IOrganizationQualificationIssuer"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("identifier", typeof(IOrganizationQualificationIdentifier), Cardinality.Multiple)]
[FhirProperty("code", typeof(IOrganizationQualificationCode), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IOrganizationQualificationPeriod), Cardinality.Optional)]
[FhirProperty("issuer", typeof(IOrganizationQualificationIssuer), Cardinality.Optional)]
public interface IOrganizationQualification : IElement;
