using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.References;

[FhirElement]
[FhirProperty("reference", typeof(IReferenceReference), Cardinality.Optional)]
[FhirProperty("type", typeof(IReferenceType), Cardinality.Optional)]
[FhirProperty("identifier", typeof(IReferenceIdentifier), Cardinality.Optional)]
[FhirProperty("display", typeof(IReferenceDisplay), Cardinality.Optional)]
public interface IReference : IElement;
