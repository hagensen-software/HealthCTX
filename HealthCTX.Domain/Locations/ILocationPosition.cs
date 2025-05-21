using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Locations;

[FhirElement]
[FhirProperty("longitude", typeof(ILocationPositionLongitude), Cardinality.Mandatory)]
[FhirProperty("latitude", typeof(ILocationPositionLatitude), Cardinality.Mandatory)]
[FhirProperty("altitude", typeof(ILocationPositionAltitude), Cardinality.Optional)]
public interface ILocationPosition : IElement;
