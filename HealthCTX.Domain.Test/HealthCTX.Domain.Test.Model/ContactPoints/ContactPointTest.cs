using HealthCTX.Domain.ContactPoints.Model;
using HealthCTX.Domain.Periods.Model;

namespace HealthCTX.Domain.Test.Model.ContactPoints;

public class ContactPointTest
{
    [Fact]
    public void ContactPoint_SerializationDeserialization_WorksCorrectly()
    {
        // Arrange
        var contactPoint = new ContactPoint(
            new ContactPointSystem("phone"),
            new ContactPointValue("123-456-7890"),
            new ContactPointUse("home"),
            new ContactPointRank(1),
            new ContactPointPeriod(
                new PeriodStart(DateTimeOffset.Now),
                new PeriodEnd(DateTimeOffset.Now.AddDays(7))));
        // Act
        var json = System.Text.Json.JsonSerializer.Serialize(contactPoint);
        var deserializedContactPoint = System.Text.Json.JsonSerializer.Deserialize<ContactPoint>(json);

        // Assert
        Assert.NotNull(deserializedContactPoint);
        Assert.Equal(contactPoint.System, deserializedContactPoint?.System);
        Assert.Equal(contactPoint.Value, deserializedContactPoint?.Value);
        Assert.Equal(contactPoint.Use, deserializedContactPoint?.Use);
        Assert.Equal(contactPoint.Rank, deserializedContactPoint?.Rank);
        Assert.Equal(contactPoint.Period?.Start, deserializedContactPoint?.Period?.Start);
        Assert.Equal(contactPoint.Period?.End, deserializedContactPoint?.Period?.End);
    }
}
