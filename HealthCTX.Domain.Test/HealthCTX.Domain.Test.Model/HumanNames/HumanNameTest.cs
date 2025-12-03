using HealthCTX.Domain.HumanNames.Model;
using HealthCTX.Domain.Periods.Model;

namespace HealthCTX.Domain.Test.Model.HumanNames;

public class HumanNameTest
{
    [Fact]
    public void HumanName_SerializationDeserialization_WorksCorrectly()
    {
        // Arrange
        var humanName = new HumanName(
            new HumanNameUse("official"),
            [new HumanNameGiven("John"),
            new HumanNameGiven("D")],
            new HumanNameFamily("Doe"),
            new HumanNamePrefix("Mr"),
            new HumanNameSuffix("Jr"),
            new HumanNameText("John D Doe"),
            new HumanNamePeriod(
                new PeriodStart(DateTimeOffset.Now),
                null));

        // Act
        var json = System.Text.Json.JsonSerializer.Serialize(humanName);
        var deserializedHumanName = System.Text.Json.JsonSerializer.Deserialize<HumanName>(json);

        // Assert
        Assert.NotNull(deserializedHumanName);
        Assert.Equal(humanName.Use, deserializedHumanName.Use);
        Assert.Equal(humanName.Givens.First(), deserializedHumanName.Givens.First());
        Assert.Equal(humanName.Givens.Last(), deserializedHumanName.Givens.Last());
        Assert.Equal(humanName.Family, deserializedHumanName.Family);
        Assert.Equal(humanName.Prefix, deserializedHumanName.Prefix);
        Assert.Equal(humanName.Suffix, deserializedHumanName.Suffix);
        Assert.Equal(humanName.Text, deserializedHumanName.Text);
        Assert.Equal(humanName.Period?.Start, deserializedHumanName.Period?.Start);
    }
}
