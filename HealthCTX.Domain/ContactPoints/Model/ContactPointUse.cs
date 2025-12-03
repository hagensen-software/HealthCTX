using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents the purpose or usage context of a contact point.
/// </summary>
[JsonConverter(typeof(ContactPointUseJsonConverter))]
public record ContactPointUse(string Value) : IContactPointUse;

/// <summary>
/// Provides a custom JSON converter for serializing and deserializing the ContactPointUse type using System.Text.Json.
/// </summary>
public class ContactPointUseJsonConverter : JsonConverter<ContactPointUse>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a ContactPointUse instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown when the JSON token is not a string or the value is null.</exception>
    public override ContactPointUse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new ContactPointUse(valueString);
    }

    /// <summary>
    /// Writes the string representation of the specified ContactPointUse value to the provided Utf8JsonWriter using the
    /// given serialization options.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, ContactPointUse value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}