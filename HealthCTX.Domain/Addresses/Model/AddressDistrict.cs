using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCTX.Domain.Addresses.Model;

/// <summary>
/// Represents a district name within an address.
/// </summary>
[JsonConverter(typeof(AddressDistrictJsonConverter))]
public record AddressDistrict(string Value) : IAddressDistrict;

/// <summary>
/// Converts AddressDistrict objects to and from their JSON string representation.
/// </summary>
public class AddressDistrictJsonConverter : JsonConverter<AddressDistrict>
{
    /// <summary>
    /// Reads and converts the JSON string representation of an address district to an AddressDistrict object.
    /// </summary>
    /// <exception cref="JsonException">Thrown if the JSON token is not a string or if the string value is null.</exception>
    public override AddressDistrict Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return valueString is null
            ? throw new JsonException("Expected a string but got null.")
            : new AddressDistrict(valueString);
    }

    /// <summary>
    /// Writes the JSON representation of the specified AddressDistrict value using the provided Utf8JsonWriter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, AddressDistrict value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}