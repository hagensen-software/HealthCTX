using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Period.Interfaces;

[FhirElement]
[FhirProperty("start", typeof(IPeriodStart), Cardinality.Optional)]
[FhirProperty("end", typeof(IPeriodEnd), Cardinality.Optional)]
public interface IPeriod : IElement
{
}
