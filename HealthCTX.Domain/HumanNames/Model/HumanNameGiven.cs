using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.HumanNames.Model;

/// <summary>
/// Represents a given (first) name component of a human's full name.
/// </summary>
[JsonConverter(typeof(HumanNameGivenJsonConverter))]
public record HumanNameGiven(string Value) : IHumanNameGiven;

/// <summary>
/// Converts between JSON string representations and HumanNameGiven objects.
/// </summary>
public class HumanNameGivenJsonConverter : JsonConverter<HumanNameGiven>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a HumanNameGiven instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown when the JSON token is not a string or the value is null.</exception>
    public override HumanNameGiven? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new HumanNameGiven(valueString);
    }

    /// <summary>
    /// Writes the given human name value as a JSON string using the specified writer.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, HumanNameGiven value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
