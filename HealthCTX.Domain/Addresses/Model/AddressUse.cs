namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a coded value indicating the intended use of an address.
/// </summary>
public record AddressUse(string Value) : IAddressUse;
