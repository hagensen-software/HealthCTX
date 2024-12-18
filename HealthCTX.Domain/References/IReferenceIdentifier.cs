using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;
using HealthCTX.Domain.Identifiers.Interfaces;

namespace HealthCTX.Domain.References;

[FhirElement]
[FhirProperty("use", typeof(IIdentifierUse), Cardinality.Single)]
[FhirProperty("type", typeof(IIdentifierType), Cardinality.Single)]
[FhirProperty("system", typeof(IIdentifierSystem), Cardinality.Single)]
[FhirProperty("value", typeof(IIdentifierValue), Cardinality.Single)]
[FhirProperty("period", typeof(IIdentifierPeriod), Cardinality.Single)]
[FhirProperty("assigner", typeof(IIdentifierAssigner), Cardinality.Single)]
public interface IReferenceIdentifier : IElement
{
}
