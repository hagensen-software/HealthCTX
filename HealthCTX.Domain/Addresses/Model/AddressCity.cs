using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents the city component of an address.
/// </summary>
[JsonConverter(typeof(AddressCityJsonConverter))]
public record AddressCity(string Value) : IAddressCity;

/// <summary>
/// Converts AddressCity values to and from their JSON string representation.
/// </summary>
public class AddressCityJsonConverter : JsonConverter<AddressCity>
{
    /// <summary>
    /// Reads and converts the JSON string representation of a city name to an AddressCity object.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override AddressCity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressCity(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified AddressCity value using the provided Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressCity value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}