using System.Text.Json;

namespace HealthCTX.Domain.Test.Bundle.BundleLink;

public class Test
{
    [Fact]
    public void Bundle_ToFhirJsonGeneratesJsonString()
    {
        var bundle = new Bundle(
            new BundleType("document"),
            [new BundleLink(
                new BundleLinkRelation("original"),
                new BundleLinkUrl(new Uri("http://www.somesource.com/document")))]);

        (var jsonString, _) = BundleFhirJsonMapper.ToFhirJsonString(bundle);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var link = root
            .GetProperty("link")
            .EnumerateArray().First();

        Assert.Equal("original", link.GetProperty("relation").GetString());
        Assert.Equal("http://www.somesource.com/document", link.GetProperty("url").GetString());
    }

    [Fact]
    public void Bundle_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Bundle",
                "type" : "document",
                "link": [{
                    "relation": "original",
                    "url": "http://www.somesource.com/document"
                }]
            }
            """;

        (var bundle, var outcomes) = BundleFhirJsonMapper.ToBundle(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("original", bundle?.Links.First().Relation.Value);
        Assert.Equal("http://www.somesource.com/document", bundle?.Links.First().Url.Value.OriginalString);
    }
}
