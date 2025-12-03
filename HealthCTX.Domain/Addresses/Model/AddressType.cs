using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a type of address.
/// </summary>
[JsonConverter(typeof(AddressTypeJsonConverter))]
public record AddressType(string Value) : IAddressType;

/// <summary>
/// Provides custom JSON serialization and deserialization for the AddressType value object.
/// </summary>
public class AddressTypeJsonConverter : JsonConverter<AddressType>
{
    /// <summary>
    /// Reads an AddressType value from the current JSON token using the specified reader and serializer options.
    /// </summary>
    /// <exception cref="JsonException">Thrown when the current JSON token is null or not a valid string representing an AddressType.</exception>
    public override AddressType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressType(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified AddressType value using the provided Utf8JsonWriter".
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
