using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.References;

/// <summary>
/// <para>Interface for the HL7 FHIR Reference element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>reference</term>
///         <description><see cref="IReferenceReference"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IReferenceType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>identifier</term>
///         <description><see cref="IReferenceIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>display</term>
///         <description><see cref="IReferenceDisplay"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("reference", typeof(IReferenceReference), Cardinality.Optional)]
[FhirProperty("type", typeof(IReferenceType), Cardinality.Optional)]
[FhirProperty("identifier", typeof(IReferenceIdentifier), Cardinality.Optional)]
[FhirProperty("display", typeof(IReferenceDisplay), Cardinality.Optional)]
public interface IReference : IElement;
