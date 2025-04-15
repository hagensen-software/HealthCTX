using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Availability;

[FhirElement]
[FhirProperty("description", typeof(IAvailabilityNotAvailableDescription), Cardinality.Optional)]
[FhirProperty("during", typeof(IAvailabilityNotAvailableDuring), Cardinality.Optional)]
public interface IAvailabilityNotAvailable : IElement;
