using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableConcepts;

[FhirElement]
[FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple)]
[FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Optional)]
public interface ICodeableConcept : IElement;
