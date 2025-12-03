using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Periods.Model;

/// <summary>
/// Represents the end point of a time period, encapsulated as a date and time value.
/// </summary>
[JsonConverter(typeof(PeriodEndJsonConverter))]
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;

/// <summary>
/// Converts PeriodEnd objects to and from their JSON string representation using System.Text.Json.
/// </summary>
public class PeriodEndJsonConverter : JsonConverter<PeriodEnd>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a PeriodEnd object using the specified serializer options.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON value is null or cannot be parsed as a valid date and time string.</exception>
    public override PeriodEnd Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString() ?? throw new JsonException("Expected a string but got null.");
        if (!DateTimeOffset.TryParse(valueString, out var dateTime))
            throw new JsonException("Invalid date time format.");

        return new PeriodEnd(DateTimeOffset.Parse(valueString));
    }

    /// <summary>
    /// Writes the specified PeriodEnd value to the JSON output using the extended ISO 8601 format.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, PeriodEnd value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value.ToString("o"));
    }
}
