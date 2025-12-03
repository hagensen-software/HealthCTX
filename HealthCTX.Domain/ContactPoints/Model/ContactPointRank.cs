namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents the rank or priority of a contact point as an immutable value object.
/// </summary>
public record ContactPointRank(uint Value) : IContactPointRank;
