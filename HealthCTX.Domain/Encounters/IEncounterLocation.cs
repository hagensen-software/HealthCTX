using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

[FhirElement]
[FhirProperty("location", typeof(IEncounterLocationLocation), Cardinality.Mandatory)]
[FhirProperty("status", typeof(IEncounterLocationStatus), Cardinality.Optional)]
[FhirProperty("physicalType", typeof(IEncounterLocationPhysicalType), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("form", typeof(IEncounterLocationForm), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("period", typeof(IEncounterLocationPeriod), Cardinality.Optional)]
public interface IEncounterLocation : IElement;
