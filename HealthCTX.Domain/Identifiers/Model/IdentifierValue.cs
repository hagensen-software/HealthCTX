using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Identifiers.Model;

/// <summary>
/// Represents an immutable identifier value.
/// </summary>
[JsonConverter(typeof(IdentifierValueJsonConverter))]
public record IdentifierValue(string Value) : IIdentifierValue;

/// <summary>
/// Converts IdentifierValue objects to and from their JSON string representation.
/// </summary>
public class IdentifierValueJsonConverter : JsonConverter<IdentifierValue>
{
    /// <summary>
    /// Reads and converts a JSON string value to an IdentifierValue object.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the current JSON token is not a string or if the string value is null.</exception>
    public override IdentifierValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new IdentifierValue(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified IdentifierValue using the provided Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, IdentifierValue value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
