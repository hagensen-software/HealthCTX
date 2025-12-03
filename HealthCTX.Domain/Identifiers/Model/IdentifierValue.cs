namespace HealthCTX.Domain.Identifiers.Model;

/// <summary>
/// Represents an immutable identifier value.
/// </summary>
public record IdentifierValue(string Value) : IIdentifierValue;
