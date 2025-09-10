using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Signatures;

/// <summary>
/// <para>Interface for the HL7 FHIR Bundle signature element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>type</term>
///         <description><see cref="ISignatureType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>when</term>
///         <description><see cref="ISignatureWhen"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>who</term>
///         <description><see cref="ISignatureWho"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>onBehalfOf</term>
///         <description><see cref="ISignatureOnBehalfOf"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>targetFormat</term>
///         <description><see cref="ISignatureTargetFormat"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>sigFormat</term>
///         <description><see cref="ISignatureSigFormat"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>data</term>
///         <description><see cref="ISignatureData"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("type", typeof(ISignatureType), Cardinality.Multiple)]
[FhirProperty("when", typeof(ISignatureWhen), Cardinality.Optional)]
[FhirProperty("who", typeof(ISignatureWho), Cardinality.Optional)]
[FhirProperty("onBehalfOf", typeof(ISignatureOnBehalfOf), Cardinality.Optional)]
[FhirProperty("targetFormat", typeof(ISignatureTargetFormat), Cardinality.Optional)]
[FhirProperty("sigFormat", typeof(ISignatureSigFormat), Cardinality.Optional)]
[FhirProperty("data", typeof(ISignatureData), Cardinality.Optional)]
public interface ISignature : IElement;
