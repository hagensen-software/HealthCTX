using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Annotation;

/// <summary>
/// <para>Interface for the HL7 FHIR Annotation element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>author[Reference]</term>
///         <description><see cref="IAnnotationAuthorReference"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>author[String]</term>
///         <description><see cref="IAnnotationAuthorString"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>time</term>
///         <description><see cref="IAnnotationTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>text</term>
///         <description><see cref="IAnnotationText"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("author[Reference]", typeof(IAnnotationAuthorReference), Cardinality.Optional)]
[FhirProperty("author[String]", typeof(IAnnotationAuthorString), Cardinality.Optional)]
[FhirProperty("time", typeof(IAnnotationTime), Cardinality.Optional)]
[FhirProperty("text", typeof(IAnnotationText), Cardinality.Mandatory)]
public interface IAnnotation : IElement;
