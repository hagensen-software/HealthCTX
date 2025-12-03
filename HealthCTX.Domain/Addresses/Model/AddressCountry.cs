using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a country component of an address, typically using a country code or name.
/// </summary>
[JsonConverter(typeof(AddressCountryJsonConverter))]

public record AddressCountry(string Value) : IAddressCountry;

/// <summary>
/// Converts between JSON string representations and AddressCountry objects for serialization and deserialization.
/// </summary>
public class AddressCountryJsonConverter : JsonConverter<AddressCountry>
{
    /// <summary>
    /// Reads and converts the JSON string representation of a city name to an AddressCountry object.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override AddressCountry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressCountry(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified AddressCountry value using the provided Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressCountry value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}