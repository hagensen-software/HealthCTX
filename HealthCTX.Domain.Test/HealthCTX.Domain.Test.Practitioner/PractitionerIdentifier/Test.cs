using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerIdentifier;

public class Test
{
    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitioner = new Practitioner(
                new PractitionerIdentifier(
                    new PractitionerIdentifierSystem(new Uri("http://some-practitioner-identifier")),
                    new PractitionerIdentifierValue("123456789")));

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJson(practitioner);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var identifiers = root.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://some-practitioner-identifier", system.GetString());
        Assert.Equal("123456789", value.GetString());
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Practitioner",
                "identifier" : [{
                    "system" : "http://some-practitioner-identifier",
                    "value" : "123456789"
                }]
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://some-practitioner-identifier", practitioner?.Identifier.System.Value.OriginalString);
        Assert.Equal("123456789", practitioner?.Identifier.Value.Value);
    }
}
