using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Timing;

[FhirElement]
[FhirProperty("boundsDuration", typeof(ITimingRepeatBoundsDuration), Cardinality.Optional)]
[FhirProperty("boundsRange", typeof(ITimingRepeatBoundsRange), Cardinality.Optional)]
[FhirProperty("boundsPeriod", typeof(ITimingRepeatBoundsPeriod), Cardinality.Optional)]
[FhirProperty("count", typeof(ITimingRepeatCount), Cardinality.Optional)]
[FhirProperty("countMax", typeof(ITimingRepeatCountMax), Cardinality.Optional)]
[FhirProperty("duration", typeof(ITimingRepeatDuration), Cardinality.Optional)]
[FhirProperty("durationMax", typeof(ITimingRepeatDurationMax), Cardinality.Optional)]
[FhirProperty("durationUnit", typeof(ITimingRepeatDurationUnit), Cardinality.Optional)]
[FhirProperty("frequency", typeof(ITimingRepeatFrequency), Cardinality.Optional)]
[FhirProperty("frequencyMax", typeof(ITimingRepeatFrequencyMax), Cardinality.Optional)]
[FhirProperty("period", typeof(ITimingRepeatPeriod), Cardinality.Optional)]
[FhirProperty("periodMax", typeof(ITimingRepeatPeriodMax), Cardinality.Optional)]
[FhirProperty("periodUnit", typeof(ITimingRepeatPeriodUnit), Cardinality.Optional)]
[FhirProperty("dayOfWeek", typeof(ITimingRepeatDayOfWeek), Cardinality.Multiple)]
[FhirProperty("timeOfDay", typeof(ITimingRepeatTimeOfDay), Cardinality.Multiple)]
[FhirProperty("when", typeof(ITimingRepeatWhen), Cardinality.Multiple)]
[FhirProperty("offset", typeof(ITimingRepeatOffset), Cardinality.Optional)]
public interface ITimingRepeat : IElement;
