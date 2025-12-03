using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Identifiers.Model;

/// <summary>
/// Represents a unique system that defines the namespace for an identifier.
/// </summary>
[JsonConverter(typeof(IdentifierSystemJsonConverter))]
public record IdentifierSystem(Uri Value) : IIdentifierSystem;

/// <summary>
/// Converts between JSON string representations and <see cref="IdentifierSystem"/> objects.
/// </summary>
public class IdentifierSystemJsonConverter : JsonConverter<IdentifierSystem>
{
    /// <summary>
    /// Reads and converts a JSON string value to an instance of IdentifierSystem.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string, the string is null, or the string is not a valid URI.</exception>
    public override IdentifierSystem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString() ?? throw new JsonException("Expected a string but got null.");
        if (!Uri.TryCreate(valueString, UriKind.RelativeOrAbsolute, out var uri))
            throw new JsonException("Invalid uri format.");

        return new IdentifierSystem(uri);
    }

    /// <summary>
    /// Writes the JSON representation of the specified IdentifierSystem value using the provided Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, IdentifierSystem value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value.ToString());
    }
}