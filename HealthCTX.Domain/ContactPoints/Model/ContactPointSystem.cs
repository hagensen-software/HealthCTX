namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents a coded system that specifies the type of contact point.
/// </summary>
public record ContactPointSystem(string Value) : IContactPointSystem;
