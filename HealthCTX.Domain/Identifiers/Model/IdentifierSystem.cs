namespace HealthCTX.Domain.Identifiers.Model;

/// <summary>
/// Represents a unique system that defines the namespace for an identifier.
/// </summary>
public record IdentifierSystem(Uri Value) : IIdentifierSystem;
