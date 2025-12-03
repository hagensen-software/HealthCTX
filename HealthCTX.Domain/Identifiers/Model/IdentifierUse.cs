using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Identifiers.Model;

/// <summary>
/// Represents a use or context of an identifier within a system.
/// </summary>
[JsonConverter(typeof(IdentifierUseJsonConverter))]
public record IdentifierUse(string Value) : IIdentifierUse;

/// <summary>
/// Converts between the IdentifierUse type and its JSON string representation.
/// </summary>
public class IdentifierUseJsonConverter : JsonConverter<IdentifierUse>
{
    /// <summary>
    /// Reads and converts the JSON string value to an IdentifierUse instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the value is null.</exception>
    public override IdentifierUse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new IdentifierUse(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified IdentifierUse value using the provided Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, IdentifierUse value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}