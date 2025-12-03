namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents the purpose or usage context of a contact point.
/// </summary>
public record ContactPointUse(string Value) : IContactPointUse;
