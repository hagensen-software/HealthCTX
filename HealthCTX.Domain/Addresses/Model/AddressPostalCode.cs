namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a postal code associated with an address.
/// </summary>
public record AddressPostalCode(string Value) : IAddressPostalCode;
