using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents a coded system that specifies the type of contact point.
/// </summary>
[JsonConverter(typeof(ContactPointSystemJsonConverter))]
public record ContactPointSystem(string Value) : IContactPointSystem;

/// <summary>
/// Provides custom JSON serialization and deserialization for the ContactPointSystem type using System.Text.Json.
/// </summary>
public class ContactPointSystemJsonConverter : JsonConverter<ContactPointSystem>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a ContactPointSystem instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override ContactPointSystem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new ContactPointSystem(valueString);
    }

    /// <summary>
    /// Writes the string representation of the specified ContactPointSystem value to the provided Utf8JsonWriter using
    /// the given serialization options.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, ContactPointSystem value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
