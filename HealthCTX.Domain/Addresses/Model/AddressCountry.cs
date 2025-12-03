namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a country component of an address, typically using a country code or name.
/// </summary>
public record AddressCountry(string Value) : IAddressCountry;
