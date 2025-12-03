namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents the city component of an address.
/// </summary>
public record AddressCity(string Value) : IAddressCity;
