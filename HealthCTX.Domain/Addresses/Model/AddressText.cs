namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a postal address as a text value.
/// </summary>
public record AddressText(string Value) : IAddressText;
