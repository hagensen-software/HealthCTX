using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.HumanNames.Model;

/// <summary>
/// Represents a coded value indicating the context or purpose for which a human name is used.
/// </summary>
[JsonConverter(typeof(HumanNameUseJsonConverter))]
public record HumanNameUse(string Value) : IHumanNameUse;

/// <summary>
/// Converts between the HumanNameUse type and its JSON string representation.
/// </summary>
public class HumanNameUseJsonConverter : JsonConverter<HumanNameUse>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a HumanNameUse instance.
    /// </summary>
    /// <exception cref="JsonException">Thrown when the JSON token is not a string or the value is null.</exception>
    public override HumanNameUse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new HumanNameUse(valueString);
    }

    /// <summary>
    /// Writes the string representation of the specified HumanNameUse value to the JSON output using the
    /// provided writer.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, HumanNameUse value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}