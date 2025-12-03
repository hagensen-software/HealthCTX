using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a coded value indicating the intended use of an address.
/// </summary>
[JsonConverter(typeof(AddressUseJsonConverter))]
public record AddressUse(string Value) : IAddressUse;

/// <summary>
/// Converts between the AddressUse type and its JSON string representation for serialization and deserialization.
/// </summary>
public class AddressUseJsonConverter : JsonConverter<AddressUse>
{
    /// <summary>
    /// Reads an AddressUse value from the current JSON token using the specified reader and serializer options.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the current JSON token is not a string or if the string value is null.</exception>
    public override AddressUse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressUse(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified AddressUse value using the provided Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressUse value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}