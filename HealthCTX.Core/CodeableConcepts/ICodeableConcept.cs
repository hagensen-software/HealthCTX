using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableConcepts;

/// <summary>
/// <para>Interface for the HL7 FHIR CodeableConcept element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>coding</term>
///         <description><see cref="ICodeableConceptCoding"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>text</term>
///         <description><see cref="ICodeableConceptText"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple)]
[FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Optional)]
public interface ICodeableConcept : IElement;
