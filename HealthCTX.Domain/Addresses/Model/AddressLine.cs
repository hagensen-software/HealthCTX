namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a single line of a postal address.
/// </summary>
public record AddressLine(string Value) : IAddressLine;
