using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationContact;

public class Test
{
    [Fact]
    public void Location_ToR4FhirJson_DoesNotGenerateContact()
    {
        var location = new Location(
            [new LocationContact(
                new ContactDetailName(
                    new ContactDetailFamilyName("Hagensen")))]);

        (var jsonString, var _) = LocationFhirJsonMapper.ToFhirJsonString(location);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.ThrowsAny<Exception>(() => root.GetProperty("contact"));
    }

    [Fact]
    public void Location_ToR5FhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new LocationContact(
                new ContactDetailName(
                    new ContactDetailFamilyName("Hagensen")))]);


        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var family = root
            .GetProperty("contact")
            .EnumerateArray().First()
            .GetProperty("name")
            .EnumerateArray().First()
            .GetProperty("family");

        Assert.Equal("Hagensen", family.GetString());
    }

    [Fact]
    public void Location_FromR4FhirJson_Fails()
    {
        var jsonString = """
            {
                "resourceType":"Organization",
                "contact":[{
                    "name":[{
                        "family":"Hagensen"
                    }]
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString);

        Assert.NotEmpty(outcomes.Issues);
    }

    [Fact]
    public void Location_FromR5FhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType":"Organization",
                "contact":[{
                    "name":[{
                        "family":"Hagensen"
                    }]
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Hagensen", location?.Contact.First().Name?.FamilyName.Value);
    }
}
