using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Availability;

[FhirElement]
[FhirProperty("daysOfWeek", typeof(IAvailabilityAvailableDaysOfWeek), Cardinality.Multiple)]
[FhirProperty("allDay", typeof(IAvailabilityAvailableAllDay), Cardinality.Optional)]
[FhirProperty("availableStartTime", typeof(IAvailabilityAvailableStartTime), Cardinality.Optional)]
[FhirProperty("availableEndTime", typeof(IAvailabilityAvailableEndTime), Cardinality.Optional)]
public interface IAvailabilityAvailable : IElement;
