using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationMode;

public record LocationMode(string Value) : ILocationMode;

public record Location(LocationMode? Mode) : ILocation;
