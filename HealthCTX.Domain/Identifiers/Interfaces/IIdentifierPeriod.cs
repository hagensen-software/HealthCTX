using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;
using HealthCTX.Domain.Period.Interfaces;

namespace HealthCTX.Domain.Identifiers.Interfaces;

[FhirElement]
[FhirProperty("start", typeof(IPeriodStart), Cardinality.Single)]
[FhirProperty("end", typeof(IPeriodEnd), Cardinality.Single)]
public interface IIdentifierPeriod : IElement;
