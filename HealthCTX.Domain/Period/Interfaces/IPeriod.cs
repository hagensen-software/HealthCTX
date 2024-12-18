using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Period.Interfaces;

[FhirElement]
[FhirProperty("start", typeof(IPeriodStart), Cardinality.Single)]
[FhirProperty("end", typeof(IPeriodEnd), Cardinality.Single)]
public interface IPeriod : IElement
{
}
