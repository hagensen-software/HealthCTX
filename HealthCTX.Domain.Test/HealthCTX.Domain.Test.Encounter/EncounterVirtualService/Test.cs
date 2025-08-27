using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterVirtualService;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonGeneratesJsonString()
    {
        var encounter = new Encounter(
            new Status("planned"),
            [new EncounterVirtualService(
                new ChannelType(
                    new ChannelTypeCode("zoom"),
                    new ChannelTypeSystem(new Uri("http://hl7.org/fhir/virtual-service-type"))),
                new AddressString("Some address"))]);

        (var jsonString, _) = encounter.ToFhirJsonString(FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var virtualService = root
            .GetProperty("virtualService")
            .EnumerateArray().First();
        var channelType = virtualService.GetProperty("channelType");
        var address = virtualService.GetProperty("addressString");

        Assert.Equal("zoom", channelType.GetProperty("code").GetString());
        Assert.Equal("http://hl7.org/fhir/virtual-service-type", channelType.GetProperty("system").GetString());
        Assert.Equal("Some address", address.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "planned",
                "virtualService" : [
                    {
                        "channelType" : {
                            "code" : "zoom",
                            "system" : "http://hl7.org/fhir/virtual-service-type"
                        },
                        "addressString" : "Some address"
                    }
                ]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("zoom", encounter?.VirtualServices.First().ChannelType?.Code?.Value);
        Assert.Equal("http://hl7.org/fhir/virtual-service-type", encounter?.VirtualServices.First().ChannelType?.System?.Value.OriginalString);
        Assert.Equal("Some address", encounter?.VirtualServices.First().Address?.Value);
    }
}
