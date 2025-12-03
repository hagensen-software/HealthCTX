using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.HumanNames.Model;

/// <summary>
/// Represents a prefix used in a human name, such as "Dr.", "Mr.", or "Ms.".
/// </summary>
[JsonConverter(typeof(HumanNamePrefixJsonConverter))]
public record HumanNamePrefix(string Value) : IHumanNamePrefix;

/// <summary>
/// Converts HumanNamePrefix objects to and from their JSON string representation.
/// </summary>
public class HumanNamePrefixJsonConverter : JsonConverter<HumanNamePrefix>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a HumanNamePrefix instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override HumanNamePrefix Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new HumanNamePrefix(valueString);
    }

    /// <summary>
    /// Writes the string representation of a HumanNamePrefix" value to the specified Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, HumanNamePrefix value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}