using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.HumanNames.Model;

/// <summary>
/// Represents the family (last) name component of a human name.
/// </summary>
[JsonConverter(typeof(HumanNamesFamilyJsonConverter))]
public record HumanNameFamily(string Value) : IHumanNameFamily;

/// <summary>
/// Converts between the JSON string representation and the HumanNameFamily type.
/// </summary>
public class HumanNamesFamilyJsonConverter : JsonConverter<HumanNameFamily>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a HumanNameFamily instance.
    /// <exception cref="JsonException">Thrown when the JSON token is not a string or the value is null.</exception>
    /// </summary>
    public override HumanNameFamily? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new HumanNameFamily(valueString);
    }

    /// <summary>
    /// Writes the JSON string representation of the specified family name value using the provided writer.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, HumanNameFamily value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
