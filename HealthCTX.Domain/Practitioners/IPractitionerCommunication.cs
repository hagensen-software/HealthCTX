using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Practitioners;

/// <summary>
/// <para>Interface for the HL7 FHIR Practitioner communication element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>coding</term>
///         <description><see cref="ICodeableConceptCoding"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>text</term>
///         <description><see cref="ICodeableConceptText"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>language</term>
///         <description><see cref="IPractitionerCommunicationLanguage"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>preferred</term>
///         <description><see cref="IPractitionerLanguagePreferred"/> (HL7 FHIR R5 Only)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("language", typeof(IPractitionerCommunicationLanguage), Cardinality.Mandatory, FromVersion: FhirVersion.R5)]
[FhirProperty("preferred", typeof(IPractitionerLanguagePreferred), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IPractitionerCommunication : IElement;
