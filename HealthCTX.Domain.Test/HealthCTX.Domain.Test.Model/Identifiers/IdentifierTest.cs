using HealthCTX.Domain.Identifiers.Model;
using HealthCTX.Domain.Periods.Model;

namespace HealthCTX.Domain.Test.Model.Identifiers;

public class IdentifierTest
{
    [Fact]
    public void Identifier_SerializationDeserialization_WorksCorrectly()
    {
        // Arrange
        var identifier = new Identifier(
            new IdentifierUse("official"),
            new IdentifierSystem(new Uri("http://hospital.org/mrn")),
            new IdentifierValue("123456"),
            new IdentifierPeriod(
                new PeriodStart(DateTimeOffset.Now),
                null));

        // Act
        var json = System.Text.Json.JsonSerializer.Serialize(identifier);
        var deserializedIdentifier = System.Text.Json.JsonSerializer.Deserialize<Identifier>(json);

        // Assert
        Assert.NotNull(deserializedIdentifier);
        Assert.Equal(identifier.Use, deserializedIdentifier?.Use);
        Assert.Equal(identifier.System, deserializedIdentifier?.System);
        Assert.Equal(identifier.Value, deserializedIdentifier?.Value);
        Assert.Equal(identifier.Period?.Start, deserializedIdentifier?.Period?.Start);
    }
}
