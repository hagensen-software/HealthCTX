using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.References;

[FhirElement]
[FhirProperty("reference", typeof(IReferenceReference), Cardinality.Single)]
[FhirProperty("type", typeof(IReferenceType), Cardinality.Single)]
[FhirProperty("identifier", typeof(IReferenceIdentifier), Cardinality.Single)]
[FhirProperty("display", typeof(IReferenceDisplay), Cardinality.Single)]
public interface IReference : IElement;
