using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationAlias;

public record LocationAlias(string Value) : ILocationAlias;

public record Location(ImmutableList<LocationAlias> Aliasses) : ILocation;