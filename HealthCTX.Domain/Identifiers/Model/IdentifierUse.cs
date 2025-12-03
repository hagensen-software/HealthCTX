namespace HealthCTX.Domain.Identifiers.Model;

/// <summary>
/// Represents a use or context of an identifier within a system.
/// </summary>
public record IdentifierUse(string Value) : IIdentifierUse;
