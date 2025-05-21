using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationStatus;

public record Status(string Value) : ILocationStatus;

public record Location(Status? Status) : ILocation;
