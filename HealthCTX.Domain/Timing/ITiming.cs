using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Timing;

[FhirElement]
[FhirProperty("event", typeof(ITimingEvent), Cardinality.Multiple)]
[FhirProperty("repeat", typeof(ITimingRepeat), Cardinality.Optional)]
[FhirProperty("code", typeof(ITimingCode), Cardinality.Optional)]
public interface ITiming : IElement;
