using HealthCTX.Domain.OperationOutcomes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Bundle.BundleEntry;

public class Test
{
    private readonly DateTimeOffset testDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string testDateString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Bundle_ToFhirJsonGeneratesJsonString()
    {
        var bundle = new Bundle(
            new BundleType("document"),
            [new BundleEntry(
                [new EntryLink(
                    new EntryLinkRelation("self"),
                    new EntryLinkUrl(new Uri("http://www.somesource.com/document")))],
                new EntryFullUrl(new Uri("http://www.somesource.com/document")),
                EntryResource.Create(
                    new Patient(
                        new PatientId("Patient/123"))),
                new EntrySearch(
                    new EntrySearchMode("match"),
                    new EntrySearchScore(1.0)),
                new EntryRequest(
                    new EntryRequestMethod("GET"),
                    new EntryRequestUrl(new Uri("http://www.somesource.com/document")),
                    new EntryRequestIfNoneMatch("*"),
                    new EntryRequestIfModifiedSince(testDate),
                    new EntryRequestIfMatch("W/\"1\""),
                    new EntryRequestIfNoneExist("Patient/123")),
                new EntryResponse(
                    new EntryResponseStatus("200 OK"),
                    new EntryResponseLocation(new Uri("http://www.somesource.com/document")),
                    new EntryResponseEtag("W/\"1\""),
                    new EntryResponseLastModified(testDate),
                    EntryResponseOutcome.Create(
                        new OperationOutcome(
                            [new OutcomeIssue(
                                new OutcomeCode("not-supported"),
                                new OutcomeDetails(
                                    new OutcomeText("This combination is not supported by the system")))]))))]);

        (var jsonString, _) = BundleFhirJsonMapper.ToFhirJsonString(bundle);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var entry = root.GetProperty("entry")
            .EnumerateArray().First();
        var link = entry.GetProperty("link")
            .EnumerateArray().First();
        var patient = entry.GetProperty("resource");
        var search = entry.GetProperty("search");
        var request = entry.GetProperty("request");
        var response = entry.GetProperty("response");
        var outcomeIssue = response.GetProperty("outcome")
            .GetProperty("issue")
            .EnumerateArray().First();

        Assert.Equal("self", link.GetProperty("relation").GetString());
        Assert.Equal("http://www.somesource.com/document", link.GetProperty("url").GetString());
        Assert.Equal("http://www.somesource.com/document", entry.GetProperty("fullUrl").GetString());
        Assert.Equal("Patient/123", patient.GetProperty("id").GetString());
        Assert.Equal("match", search.GetProperty("mode").GetString());
        Assert.Equal(1.0, search.GetProperty("score").GetDouble());
        Assert.Equal("GET", request.GetProperty("method").GetString());
        Assert.Equal("http://www.somesource.com/document", request.GetProperty("url").GetString());
        Assert.Equal("*", request.GetProperty("ifNoneMatch").GetString());
        Assert.Equal(testDateString, request.GetProperty("ifModifiedSince").GetString());
        Assert.Equal("W/\"1\"", request.GetProperty("ifMatch").GetString());
        Assert.Equal("Patient/123", request.GetProperty("ifNoneExist").GetString());
        Assert.Equal("200 OK", response.GetProperty("status").GetString());
        Assert.Equal("http://www.somesource.com/document", response.GetProperty("location").GetString());
        Assert.Equal("W/\"1\"", response.GetProperty("etag").GetString());
        Assert.Equal(testDateString, response.GetProperty("lastModified").GetString());
        Assert.Equal("not-supported", outcomeIssue.GetProperty("code").GetString());
    }

    [Fact]
    public void Bundle_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Bundle",
                "type" : "document",
                "entry" : [{
                    "link": [{
                        "relation": "self",
                        "url": "http://www.somesource.com/document"
                    }],
                    "fullUrl": "http://www.somesource.com/document",
                    "resource" : {
                        "resourceType" : "Patient",
                        "id" : "Patient/123"
                    },
                    "search": {
                        "mode": "match",
                        "score": 1.0
                    },
                    "request": {
                        "method": "GET",
                        "url": "http://www.somesource.com/document",
                        "ifNoneMatch": "*",
                        "ifModifiedSince": "{{testDateString}}",
                        "ifMatch": "W/\"1\"",
                        "ifNoneExist": "Patient/123"
                    },
                    "response": {
                        "status": "200 OK",
                        "location": "http://www.somesource.com/document",
                        "etag": "W/\"1\"",
                        "lastModified": "{{testDateString}}",
                        "outcome" : {
                            "resourceType" : "OperationOutcome",
                            "issue" : [{
                                "code" : "not-supported",
                                "details" : {
                                    "text" : "This combination is not supported by the system"
                                }
                            }]
                        }
                    }
                }]
            }
            """;

        (var bundle, var outcomes) = BundleFhirJsonMapper.ToBundle(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("self", bundle?.Entries.First().Links.First().Relation.Value);
        Assert.Equal("http://www.somesource.com/document", bundle?.Entries.First().Links.First().Url.Value.OriginalString);
        Assert.Equal("http://www.somesource.com/document", bundle?.Entries.First().FullUrl.Value.OriginalString);
        Assert.Equal("Patient/123", (bundle?.Entries.First().Resource?.Value as Patient)?.Id.Value);
        Assert.Equal("match", bundle?.Entries.First().Search?.Mode?.Value);
        Assert.Equal(1.0, bundle?.Entries.First().Search?.Score?.Value);
        Assert.Equal("GET", bundle?.Entries.First().Request?.Method.Value);
        Assert.Equal("http://www.somesource.com/document", bundle?.Entries.First().Request?.Url.Value.OriginalString);
        Assert.Equal("*", bundle?.Entries.First().Request?.IfNoneMatch?.Value);
        Assert.Equal(testDate, bundle?.Entries.First().Request?.IfModifiedSince?.Value);
        Assert.Equal("W/\"1\"", bundle?.Entries.First().Request?.IfMatch?.Value);
        Assert.Equal("Patient/123", bundle?.Entries.First().Request?.IfNoneExist?.Value);
        Assert.Equal("200 OK", bundle?.Entries.First().Response?.Status?.Value);
        Assert.Equal("http://www.somesource.com/document", bundle?.Entries.First().Response?.Location?.Value.OriginalString);
        Assert.Equal("W/\"1\"", bundle?.Entries.First().Response?.Etag?.Value);
        Assert.Equal(testDate, bundle?.Entries.First().Response?.LastModified?.Value);
        Assert.Equal("not-supported", (bundle?.Entries.First().Response?.Outcome?.Value as OperationOutcome)?.Issues.First().Code.Value);
    }
}
