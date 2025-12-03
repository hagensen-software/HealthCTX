using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.HumanNames.Model;

/// <summary>
/// Represents the textual portion of a human name, such as a given name, family name, or other name component.
/// </summary>
[JsonConverter(typeof(HumanNameTextJsonConverter))]
public record HumanNameText(string Value) : IHumanNameText;

/// <summary>
/// Converts HumanNameText objects to and from their JSON string representation.
/// </summary>
public class HumanNameTextJsonConverter : JsonConverter<HumanNameText>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a HumanNameText instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override HumanNameText? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new HumanNameText(valueString);
    }

    /// <summary>
    /// Writes the string representation of a HumanNameText value to the specified Utf8JsonWriter as a JSON string.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, HumanNameText value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
