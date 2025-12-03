using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.ContactPoints.Model;

/// <summary>
/// Represents the rank or priority of a contact point as an immutable value object.
/// </summary>
[JsonConverter(typeof(ContactPointJsonConverter))]
public record ContactPointRank(uint Value) : IContactPointRank;

/// <summary>
/// Converts ContactPointRank values to and from their JSON representation as unsigned integers.
/// </summary>
public class ContactPointJsonConverter : JsonConverter<ContactPointRank>
{
    /// <summary>
    /// Reads a JSON number and converts it to a ContactPointRank value.
    /// </summary>
    public override ContactPointRank Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number || !reader.TryGetUInt32(out uint value))
            throw new JsonException("Invalid JSON value for ContactPointRank, expected unsigned integer.");

        return new ContactPointRank(value);
    }

    /// <summary>
    /// Writes the numeric value of the specified ContactPointRank to the provided Utf8JsonWriter as a JSON number.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, ContactPointRank value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Value);
    }
}