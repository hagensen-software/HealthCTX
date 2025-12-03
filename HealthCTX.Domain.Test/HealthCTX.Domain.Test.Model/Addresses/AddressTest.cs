using HealthCTX.Domain.Addresses.Model;
using HealthCTX.Domain.Periods.Model;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Model.Addresses;

public class AddressTest
{
    [Fact]
    public void Address_SerializationDeserialization_WorksCorrectly()
    {
        // Arrange
        var address = new Address(
            [new AddressLine("Address line 1"),
            new AddressLine("Address line 2")],
            new AddressPostalCode("Postal Code"),
            new AddressCity("City"),
            new AddressDistrict("District"),
            new AddressCountry("Country"),
            new AddressState("State"),
            new AddressType("Type"),
            new AddressUse("Use"),
            new AddressText("Full Address Text"),
            new AddressPeriod(
                new PeriodStart(DateTimeOffset.Now),
                new PeriodEnd(DateTimeOffset.Now.AddDays(7))));

        // Act
        var json = JsonSerializer.Serialize(address);
        var deserializedAddress = JsonSerializer.Deserialize<Address>(json);

        // Assert
        Assert.NotNull(deserializedAddress);
        Assert.Equal(address.Lines.First(), deserializedAddress?.Lines.First());
        Assert.Equal(address.Lines.Last(), deserializedAddress?.Lines.Last());
        Assert.Equal(address.PostalCode, deserializedAddress?.PostalCode);
        Assert.Equal(address.City, deserializedAddress?.City);
        Assert.Equal(address.District, deserializedAddress?.District);
        Assert.Equal(address.Country, deserializedAddress?.Country);
        Assert.Equal(address.State, deserializedAddress?.State);
        Assert.Equal(address.Type, deserializedAddress?.Type);
        Assert.Equal(address.Use, deserializedAddress?.Use);
        Assert.Equal(address.Text, deserializedAddress?.Text);
        Assert.Equal(address.Period?.Start, deserializedAddress?.Period?.Start);
        Assert.Equal(address.Period?.End, deserializedAddress?.Period?.End);
    }
}
