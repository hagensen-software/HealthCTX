using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationDescription;

public record LocationDescription(string Value) : ILocationDescription;

public record Location(LocationDescription Description) : ILocation;
