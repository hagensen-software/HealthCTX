using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Practitioners;

/// <summary>
/// <para>Interface for the HL7 FHIR Practitioner resource.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>identifier</term>
///         <description><see cref="IPractitionerIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>active</term>
///         <description><see cref="IPractitionerActive"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>name</term>
///         <description><see cref="IPractitionerHumanName"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>telecom</term>
///         <description><see cref="IPractitionerTelecom"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address</term>
///         <description><see cref="IPractitionerAddress"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>gender</term>
///         <description><see cref="IPractitionerGender"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>birthDate</term>
///         <description><see cref="IPractitionerBirthDate"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>photo</term>
///         <description><see cref="IPractitionerPhoto"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>qualification</term>
///         <description><see cref="IPractitionerQualification"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>communication</term>
///         <description><see cref="IPractitionerCommunication"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirResource("Practitioner")]
[FhirProperty("identifier",typeof(IPractitionerIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPractitionerActive), Cardinality.Optional)]
[FhirProperty("name", typeof(IPractitionerHumanName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IPractitionerTelecom), Cardinality.Multiple)]
[FhirProperty("address", typeof(IPractitionerAddress), Cardinality.Multiple)]
[FhirProperty("gender", typeof(IPractitionerGender), Cardinality.Optional)]
[FhirProperty("birthDate", typeof(IPractitionerBirthDate), Cardinality.Optional)]
[FhirProperty("photo", typeof(IPractitionerPhoto), Cardinality.Multiple)]
[FhirProperty("qualification", typeof(IPractitionerQualification), Cardinality.Multiple)]
[FhirProperty("communication", typeof(IPractitionerCommunication), Cardinality.Multiple)]
public interface IPractitioner : IResource;
