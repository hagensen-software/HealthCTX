namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents the state of an address as a value object.
/// </summary>
public record AddressState(string Value) : IAddressState;
