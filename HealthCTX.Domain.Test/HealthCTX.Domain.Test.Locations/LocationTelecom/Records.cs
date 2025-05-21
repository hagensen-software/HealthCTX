using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationTelecom;

public record TelecomValue(string Value) : IContactPointValue;
public record TelecomSystem(string Value) : IContactPointSystem;
public record LocationTelecom(TelecomSystem? System, TelecomValue? Value) : ILocationTelecom;

public record Location(ImmutableList<LocationTelecom> Telecoms) : ILocation;
