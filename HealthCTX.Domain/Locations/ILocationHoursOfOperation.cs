using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Availability;

namespace HealthCTX.Domain.Locations;

[FhirElement]
[FhirProperty("availableTime", typeof(IAvailabilityAvailable), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("notAvailableTime", typeof(IAvailabilityNotAvailable), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("daysOfWeek", typeof(IAvailabilityAvailableDaysOfWeek), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("allDay", typeof(IAvailabilityAvailableAllDay), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("openingTime", typeof(ILocationHoursOfOperationOpeningTime), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("closingTime", typeof(ILocationHoursOfOperationClosingTime), Cardinality.Optional, ToVersion: FhirVersion.R4)]
public interface ILocationHoursOfOperation : IElement;
