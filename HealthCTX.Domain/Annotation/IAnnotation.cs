using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Annotation;

[FhirElement]
[FhirProperty("author[Reference]", typeof(IAnnotationAuthorReference), Cardinality.Optional)]
[FhirProperty("author[String]", typeof(IAnnotationAuthorString), Cardinality.Optional)]
[FhirProperty("time", typeof(IAnnotationTime), Cardinality.Optional)]
[FhirProperty("text", typeof(IAnnotationText), Cardinality.Mandatory)]
public interface IAnnotation : IElement;
