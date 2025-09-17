using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableConcepts;

/// <summary>
/// <para>Interface for HL7 FHIR CodeableConcept coding.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>system</term>
///         <description><see cref="ICodingSystem"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>version</term>
///         <description><see cref="ICodingVersion"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>code</term>
///         <description><see cref="ICodingCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>display</term>
///         <description><see cref="ICodingDisplay"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>userSelected</term>
///         <description><see cref="ICodingUserSelected"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("system", typeof(ICodingSystem), Cardinality.Optional)]
[FhirProperty("version", typeof(ICodingVersion), Cardinality.Optional)]
[FhirProperty("code", typeof(ICodingCode), Cardinality.Optional)]
[FhirProperty("display", typeof(ICodingDisplay), Cardinality.Optional)]
[FhirProperty("userSelected", typeof(ICodingUserSelected), Cardinality.Optional)]
public interface ICodeableConceptCoding : IElement;
