using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.VirtualServiceDetail;

namespace HealthCTX.Domain.Locations;

[FhirResource("Location")]
[FhirProperty("identifier", typeof(ILocationIdentifier), Cardinality.Multiple)]
[FhirProperty("status", typeof(ILocationStatus), Cardinality.Optional)]
[FhirProperty("operationalStatus", typeof(ILocationOperationalStatus), Cardinality.Optional)]
[FhirProperty("name", typeof(ILocationName), Cardinality.Optional)]
[FhirProperty("alias", typeof(ILocationAlias), Cardinality.Multiple)]
[FhirProperty("description", typeof(ILocationDescription), Cardinality.Optional)]
[FhirProperty("mode", typeof(ILocationMode), Cardinality.Optional)]
[FhirProperty("type", typeof(ILocationType), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(ILocationTelecom), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("contact", typeof(ILocationContact), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("address", typeof(ILocationAddress), Cardinality.Optional)]
[FhirProperty("physicalType", typeof(ILocationPhysicalType), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("form", typeof(ILocationForm), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("position", typeof(ILocationPosition), Cardinality.Optional)]
[FhirProperty("managingOrganization", typeof(ILocationManagingOrganization), Cardinality.Optional)]
[FhirProperty("partOf", typeof(ILocationPartOf), Cardinality.Optional)]
[FhirProperty("characteristic", typeof(ILocationCharacteristic), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("hoursOfOperation", typeof(ILocationHoursOfOperation), Cardinality.Multiple)]
[FhirProperty("availabilityExceptions", typeof(ILocationAvailabilityExceptions), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("virtualService", typeof(IVirtualServiceDetail), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("endpoint", typeof(ILocationEndpoint), Cardinality.Multiple)]
public interface ILocation : IResource;
