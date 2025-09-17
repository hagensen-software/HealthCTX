using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Practitioners;

/// <summary>
/// <para>Interface for the HL7 FHIR Practitioner qualification element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>identifier</term>
///         <description><see cref="IPractitionerQualificationIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>code</term>
///         <description><see cref="IPractitionerQualificationCodeableConcept"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IPractitionerQualificationPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>issuer</term>
///         <description><see cref="IPractitionerQualificationIssuer"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("identifier", typeof(IPractitionerQualificationIdentifier), Cardinality.Multiple)]
[FhirProperty("code", typeof(IPractitionerQualificationCodeableConcept), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IPractitionerQualificationPeriod), Cardinality.Optional)]
[FhirProperty("issuer", typeof(IPractitionerQualificationIssuer), Cardinality.Optional)]
public interface IPractitionerQualification : IElement;
