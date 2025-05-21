using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationPosition;

public record Longitude(double Value) : ILocationPositionLongitude;
public record Latutude(double Value) : ILocationPositionLatitude;
public record Altitude(double Value) : ILocationPositionAltitude;

public record LocationPosition(
    Longitude Longitude,
    Latutude Latitude,
    Altitude? Altitude) : ILocationPosition;

public record Location(LocationPosition? Position) : ILocation;
