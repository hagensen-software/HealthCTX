using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Identifiers.Interfaces;

[FhirElement]
[FhirProperty("use", typeof(IIdentifierUse), Cardinality.Optional)]
[FhirProperty("type", typeof(IIdentifierType), Cardinality.Optional)]
[FhirProperty("system", typeof(IIdentifierSystem), Cardinality.Optional)]
[FhirProperty("value", typeof(IIdentifierValue), Cardinality.Optional)]
[FhirProperty("period", typeof(IIdentifierPeriod), Cardinality.Optional)]
[FhirProperty("assigner", typeof(IIdentifierAssigner), Cardinality.Optional)]
public interface IIdentifier : IElement;
