using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableReferences;

[FhirElement]
[FhirProperty("concept", typeof(ICodeableReferenceConcept), Cardinality.Optional)]
[FhirProperty("reference", typeof(ICodeableReferenceReference), Cardinality.Optional)]
public interface ICodeableReference : IElement;
