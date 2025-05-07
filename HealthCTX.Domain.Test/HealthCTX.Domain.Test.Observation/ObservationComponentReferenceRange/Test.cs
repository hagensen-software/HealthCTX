using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentReferenceRange;

public class Test
{
    [Fact]
    public void Observation_ToFhirJsonGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("35094-2"),
                new CodeSystem(new Uri("http://loinc.org")))),
            [new ObservationComponent(
                new ObservationComponentCode(
                    new ObservationComponentCoding(
                        new ComponentCode("8480-6"),
                        new ComponentCodeSystem(new Uri("http://loinc.org")))),
                [new ReferenceRange(
                    new ReferenceRangeLow(new LowValue(10.0)),
                    new ReferenceRangeHigh(new HighValue(15.0)))])]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var referenceRange = root
            .GetProperty("component")
            .EnumerateArray().First()
            .GetProperty("referenceRange")
            .EnumerateArray().First();

        Assert.Equal(10.0, referenceRange.GetProperty("low").GetProperty("value").GetDouble());
        Assert.Equal(15.0, referenceRange.GetProperty("high").GetProperty("value").GetDouble());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "code" : {
                    "coding" : [{
                        "code" : "8310-5",
                        "system" : "http://loinc.org"
                    }],
                    "text" : "Observation Code"
                },
                "component" : [{
                    "code" : {
                        "coding" : [{
                            "code" : "8480-6",
                            "system" : "http://loinc.org"
                        }]
                    },
                    "referenceRange" : [{
                        "low" : {
                            "value" : 10.0
                        },
                        "high" : {
                            "value" : 15.0
                        }
                    }]
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(10.0, observation?.Components.First().ReferenceRange.First().Low.Value.Value);
        Assert.Equal(15.0, observation?.Components.First().ReferenceRange.First().High.Value.Value);
    }
}
