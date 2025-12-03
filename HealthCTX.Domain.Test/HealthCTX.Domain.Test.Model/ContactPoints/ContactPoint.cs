using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.ContactPoints.Model;

namespace HealthCTX.Domain.Test.Model.ContactPoints;

public record ContactPoint(
    ContactPointSystem System,
    ContactPointValue Value,
    ContactPointUse Use,
    ContactPointRank Rank,
    ContactPointPeriod Period) : IContactPoint;
