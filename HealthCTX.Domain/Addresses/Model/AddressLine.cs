using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a single line of a postal address.
/// </summary>
[JsonConverter(typeof(AddressLineJsonConverter))]
public record AddressLine(string Value) : IAddressLine;

/// <summary>
/// Converts AddressLine objects to and from their JSON string representation for use with System.Text.Json
/// serialization.
/// </summary>
public class AddressLineJsonConverter : JsonConverter<AddressLine>
{
    /// <summary>
    /// Reads and converts a JSON string value to an AddressLine object.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override AddressLine Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressLine(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of an AddressLine value using the specified Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressLine value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}