using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a postal code associated with an address.
/// </summary>
[JsonConverter(typeof(AddressPostalCodeJsonConverter))]
public record AddressPostalCode(string Value) : IAddressPostalCode;

/// <summary>
/// Provides a custom JSON converter for serializing and deserializing AddressPostalCode values using System.Text.Json.
/// </summary>
public class AddressPostalCodeJsonConverter : JsonConverter<AddressPostalCode>
{
    /// <summary>
    /// Reads a JSON string value and converts it to an AddressPostalCode instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override AddressPostalCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressPostalCode(valueString);
    }

    /// <summary>
    /// Writes the postal code value to the specified JSON writer as a string.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressPostalCode value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}