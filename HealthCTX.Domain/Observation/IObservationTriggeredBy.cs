using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observation;

[FhirElement]
[FhirProperty("observation", typeof(IObservationTriggeredByObservation), Cardinality.Mandatory)]
[FhirProperty("type", typeof(IObservationTriggeredByType), Cardinality.Mandatory)]
[FhirProperty("reason", typeof(IObservationTriggeredByReason), Cardinality.Optional)]
public interface IObservationTriggeredBy : IElement;
