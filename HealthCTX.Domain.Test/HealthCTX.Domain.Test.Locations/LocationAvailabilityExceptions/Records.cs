using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationAvailabilityExceptions;

public record LocationAvailabilityExceptions(string Value) : ILocationAvailabilityExceptions;

public record Location(LocationAvailabilityExceptions AvailabilityExceptions) : ILocation;

