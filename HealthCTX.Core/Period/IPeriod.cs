using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Period;

[FhirElement]
[FhirProperty("start", typeof(IPeriodStart), Cardinality.Optional)]
[FhirProperty("end", typeof(IPeriodEnd), Cardinality.Optional)]
public interface IPeriod : IElement;
