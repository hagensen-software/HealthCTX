using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents the state of an address as a value object.
/// </summary>
[JsonConverter(typeof(AddressStateJsonConverter))]
public record AddressState(string Value) : IAddressState;

/// <summary>
/// Converts AddressState values to and from their JSON string representation.
/// /// </summary>
public class AddressStateJsonConverter : JsonConverter<AddressState>
{
    /// <summary>
    /// Reads a JSON string value and converts it to an AddressState object.
    /// </summary>
    /// <exception cref="JsonException">Thrown when the JSON token is not a string or is null.</exception>
    public override AddressState Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new System.Text.Json.JsonException("Expected a string but got null.")
            : new AddressState(valueString);
    }

    /// <summary>
    /// Writes the state value to the specified JSON writer as a string.
    /// </summary>
    public override void Write(System.Text.Json.Utf8JsonWriter writer, AddressState value, System.Text.Json.JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
