using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a postal address as a text value.
/// </summary>
[JsonConverter(typeof(AddressTextJsonConverter))]
public record AddressText(string Value) : IAddressText;

/// <summary>
/// Provides a custom JSON converter for serializing and deserializing AddressText values using System.Text.Json.
/// </summary>
public class AddressTextJsonConverter : JsonConverter<AddressText>
{
    /// <summary>
    /// Reads a JSON string value and converts it to an AddressText object.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override AddressText Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressText(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified AddressText value using the provided <see
    /// cref="Utf8JsonWriter"/>.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressText value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}