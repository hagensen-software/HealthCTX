using HealthCTX.Domain.Address;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationAddress;

public record AddressText(string Value) : IAddressText;
public record LocationAddress(AddressText Text) : ILocationAddress;
public record Location(LocationAddress? Address) : ILocation;
