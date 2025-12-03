using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents a contact point value.
/// </summary>
[JsonConverter(typeof(ContactPointValueJsonConverter))]
public record ContactPointValue(string Value) : IContactPointValue;

/// <summary>
/// Converts ContactPointValue values to and from their JSON string representation.
/// </summary>
public class ContactPointValueJsonConverter : JsonConverter<ContactPointValue>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a ContactPointValue instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override ContactPointValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new ContactPointValue(valueString);
    }

    /// <summary>
    /// Writes the string representation of the specified contact point value to the provided JSON writer.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, ContactPointValue value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
