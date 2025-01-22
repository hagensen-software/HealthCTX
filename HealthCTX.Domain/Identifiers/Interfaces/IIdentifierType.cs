using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Identifiers.Interfaces;

[FhirElement]
[FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple)]
[FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Optional)]
public interface IIdentifierType : IElement;
