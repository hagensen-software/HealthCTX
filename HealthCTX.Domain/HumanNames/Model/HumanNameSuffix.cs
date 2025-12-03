using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.HumanNames.Model;

/// <summary>
/// Represents a suffix component of a human name.
/// </summary>
[JsonConverter(typeof(HumanNameSuffixJsonConverter))]
public record HumanNameSuffix(string Value) : IHumanNameSuffix;

/// <summary>
/// Converts between JSON string representations and HumanNameSuffix objects.
/// </summary>
public class HumanNameSuffixJsonConverter : JsonConverter<HumanNameSuffix>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a HumanNameSuffix instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown when the JSON token is not a string or the value is null.</exception>
    public override HumanNameSuffix Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new HumanNameSuffix(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of a HumanNameSuffix value using the specified Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, HumanNameSuffix value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}