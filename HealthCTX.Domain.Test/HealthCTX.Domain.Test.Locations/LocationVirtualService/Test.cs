using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Locations.LocationVirtualService;

public class Test
{
    [Fact]
    public void Location_ToFhirJsonGeneratesJsonString()
    {
        var location = new Location(
            [new VirtualServiceDetail(
                new ChannelType(
                    new ChannelTypeCode("zoom"),
                    new ChannelTypeSystem(new Uri("http://hl7.org/fhir/virtual-service-type"))),
                new AddressString("Some Address"),
                new AdditionalInfo(new Uri("http://someurl.org")),
                new MaxParticipants(7),
                new SessionKey("1234567890"))]);

        (var jsonString, _) = LocationFhirJsonMapper.ToFhirJsonString(location, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var virtualService = root
            .GetProperty("virtualService")
            .EnumerateArray().First();

        Assert.Equal("zoom", virtualService.GetProperty("channelType").GetProperty("code").GetString());
        Assert.Equal("Some Address", virtualService.GetProperty("addressString").GetString());
        Assert.Equal("http://someurl.org", virtualService.GetProperty("additionalInfo").GetString());
        Assert.Equal(7, virtualService.GetProperty("maxParticipants").GetInt32());
        Assert.Equal("1234567890", virtualService.GetProperty("sessionKey").GetString());
    }

    [Fact]
    public void Location_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Location",
                "virtualService" : [{
                    "channelType" : {
                        "code" : "zoom",
                        "system" : "http://hl7.org/fhir/virtual-service-type"
                    },
                    "addressString" : "Some Address",
                    "additionalInfo" : "http://someurl.org",
                    "maxParticipants" : 7,
                    "sessionKey" : "1234567890"
                }]
            }
            """;

        (var location, var outcomes) = LocationFhirJsonMapper.ToLocation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("zoom", location?.VirtualServiceDetail?.First().ChannelType?.Code?.Value);
        Assert.Equal("Some Address", location?.VirtualServiceDetail?.First().Address?.Value);
        Assert.Equal("http://someurl.org", location?.VirtualServiceDetail?.First().AdditionalInfo?.Value.OriginalString);
        Assert.Equal(7U, location?.VirtualServiceDetail?.First().MaxParticipants?.Value);
        Assert.Equal("1234567890", location?.VirtualServiceDetail?.First().SessionKey?.Value);
    }
}
