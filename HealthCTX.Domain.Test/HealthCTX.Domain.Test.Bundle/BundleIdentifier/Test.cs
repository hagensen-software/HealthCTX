using System.Text.Json;

namespace HealthCTX.Domain.Test.Bundle.BundleIdentifier;

public class Test
{
    [Fact]
    public void Bundle_ToFhirJsonGeneratesJsonString()
    {
        var bundle = new Bundle(
            new BundleType("document"),
            new Identifier(
                new IdentifierSystem(new Uri("http://some-bundle-identifier-system")),
                new IdentifierValue("12345678")));

        (var jsonString, _) = BundleFhirJsonMapper.ToFhirJsonString(bundle);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var identifier = root.GetProperty("identifier");
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://some-bundle-identifier-system", system.GetString());
        Assert.Equal("12345678", value.GetString());
    }

    [Fact]
    public void Bundle_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Bundle",
                "type" : "document",
                "identifier" : {
                    "system" : "http://some-bundle-identifier-system",
                    "value" : "12345678"
                }
            }
            """;

        (var bundle, var outcomes) = BundleFhirJsonMapper.ToBundle(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://some-bundle-identifier-system", bundle?.Identifier?.System?.Value.OriginalString);
        Assert.Equal("12345678", bundle?.Identifier?.Value?.Value);
    }
}
