using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirElement]
[FhirProperty("coding", typeof(ICoding))]
[FhirProperty("text", typeof(ICodeableConceptText))]
public interface IMaritalStatusCodeableConcept : IElement
{
}
