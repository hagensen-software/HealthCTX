using System.Text.Json;

namespace HealthCTX.Domain.Test.Bundle.BundleType;

public class Test
{
    [Fact]
    public void Bundle_ToFhirJsonGeneratesJsonString()
    {
        var bundle = new Bundle(
            new BundleType("document"));

        (var jsonString, _) = BundleFhirJsonMapper.ToFhirJsonString(bundle);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var type = root.GetProperty("type");

        Assert.Equal("document", type.GetString());
    }

    [Fact]
    public void Bundle_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Bundle",
                "type" : "document"
            }
            """;

        (var bundle, var outcomes) = BundleFhirJsonMapper.ToBundle(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("document", bundle?.Type.Value);
    }
}
