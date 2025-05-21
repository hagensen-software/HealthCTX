using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationName;

public record LocationName(string Value) : ILocationName;

public record Location(LocationName? Name) : ILocation;
