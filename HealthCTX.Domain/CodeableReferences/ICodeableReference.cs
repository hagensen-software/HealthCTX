using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableReferences;

/// <summary>
/// <para>Interface for the HL7 FHIR ELEMENT ATTRIBUTE element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>concept</term>
///         <description><see cref="ICodeableReferenceConcept"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>reference</term>
///         <description><see cref="ICodeableReferenceReference"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("concept", typeof(ICodeableReferenceConcept), Cardinality.Optional)]
[FhirProperty("reference", typeof(ICodeableReferenceReference), Cardinality.Optional)]
public interface ICodeableReference : IElement;
