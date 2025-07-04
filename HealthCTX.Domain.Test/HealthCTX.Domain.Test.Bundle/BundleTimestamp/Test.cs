using System.Text.Json;

namespace HealthCTX.Domain.Test.Bundle.BundleTimestamp;

public class Test
{
    private readonly DateTimeOffset testDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string testDateString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Bundle_ToFhirJsonGeneratesJsonString()
    {
        var bundle = new Bundle(
            new BundleType("document"),
            new Timestamp(testDate));

        (var jsonString, _) = BundleFhirJsonMapper.ToFhirJsonString(bundle);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var timestamp = root.GetProperty("timestamp");

        Assert.Equal(testDateString, timestamp.GetString());
    }

    [Fact]
    public void Bundle_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Bundle",
                "type" : "document",
                "timestamp" : "{{testDateString}}"
            }
            """;

        (var bundle, var outcomes) = BundleFhirJsonMapper.ToBundle(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testDate, bundle?.Timestamp?.Value);
    }
}
