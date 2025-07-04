using HealthCTX.Domain.Test.Bundle.BundleTimestamp;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Bundle.BundleTotal;

public class Test
{
    [Fact]
    public void Bundle_ToFhirJsonGeneratesJsonString()
    {
        var bundle = new Bundle(
            new BundleType("document"),
            new Total(12));

        (var jsonString, _) = BundleFhirJsonMapper.ToFhirJsonString(bundle);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var total = root.GetProperty("total");

        Assert.Equal(12U, total.GetUInt32());
    }

    [Fact]
    public void Bundle_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Bundle",
                "type" : "document",
                "total" : 12
            }
            """;

        (var bundle, var outcomes) = BundleFhirJsonMapper.ToBundle(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(12U, bundle?.Total?.Value);
    }
}
