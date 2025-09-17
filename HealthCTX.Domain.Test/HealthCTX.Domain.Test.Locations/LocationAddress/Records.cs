using HealthCTX.Domain.Addresses;
using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationAddress;

public record AddressText(string Value) : IAddressText;
public record LocationAddress(AddressText Text) : ILocationAddress;
public record Location(LocationAddress? Address) : ILocation;
