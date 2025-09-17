using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.ExtendedContactDetails;

/// <summary>
/// <para>Interface for the HL7 FHIR ExtendedContactDetail element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>purpose</term>
///         <description><see cref="IExtendedContactDetailPurpose"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>name</term>
///         <description><see cref="IExtendedContactDetailName"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>telecom</term>
///         <description><see cref="IExtendedContactDetailTelecom"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address</term>
///         <description><see cref="IExtendedContactDetailAddress"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>organization</term>
///         <description><see cref="IExtendedContactDetailOrganization"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IExtendedContactDetailPeriod"/> (HL7 FHIR R5 Only)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("purpose", typeof(IExtendedContactDetailPurpose), Cardinality.Optional)]
[FhirProperty("name", typeof(IExtendedContactDetailName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IExtendedContactDetailTelecom), Cardinality.Multiple)]
[FhirProperty("address", typeof(IExtendedContactDetailAddress), Cardinality.Optional)]
[FhirProperty("organization", typeof(IExtendedContactDetailOrganization), Cardinality.Optional, FhirVersion.R5)]
[FhirProperty("period", typeof(IExtendedContactDetailPeriod), Cardinality.Optional, FhirVersion.R5)]
public interface IExtendedContactDetail : IElement;
