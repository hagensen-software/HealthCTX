using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.CodeableConcepts.Interfaces;

[FhirElement]
[FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple)]
[FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Single)]
public interface ICodeableConcept : IElement;
