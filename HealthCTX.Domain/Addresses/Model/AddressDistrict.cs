namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a district name within an address.
/// </summary>
public record AddressDistrict(string Value) : IAddressDistrict;
