using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Availability;

[FhirElement]
[FhirProperty("availableTime", typeof(IAvailabilityAvailable), Cardinality.Multiple)]
[FhirProperty("notAvailableTime", typeof(IAvailabilityNotAvailable), Cardinality.Multiple)]
public interface IAvailability : IElement;
