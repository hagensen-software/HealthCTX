namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents a contact point value.
/// </summary>
public record ContactPointValue(string Value) : IContactPointValue;
