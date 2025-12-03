using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Periods.Model;

/// <summary>
/// Represents the starting point of a time period using a specific date and time offset.
/// </summary>
[JsonConverter(typeof(PeriodStartJsonConverter))]
public record PeriodStart(DateTimeOffset Value) : IPeriodStart;

/// <summary>
/// Converts PeriodStart values to and from their JSON string representation using the System.Text.Json serialization
/// framework.
/// </summary>
public class PeriodStartJsonConverter : JsonConverter<PeriodStart>
{
    /// <summary>
    /// Reads a JSON string value and converts it to a PeriodStart instance representing the specified date and time.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON value is null or cannot be parsed as a valid date and time string.</exception>
    public override PeriodStart Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString() ?? throw new JsonException("Expected a string but got null.");
        if (!DateTimeOffset.TryParse(valueString, out var dateTime))
            throw new JsonException("Invalid date time format.");

        return new PeriodStart(DateTimeOffset.Parse(valueString));
    }

    /// <summary>
    /// Writes the specified <see cref="PeriodStart"/> value as a JSON string using the extended ISO 8601 format.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, PeriodStart value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value.ToString("o"));
    }
}
