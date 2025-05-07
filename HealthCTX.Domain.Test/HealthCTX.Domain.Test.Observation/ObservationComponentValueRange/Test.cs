using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValueRange;

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
            new ValueRange(
                new ObservationRangeLow(new LowValue(10.0)),
                new ObservationRangeHigh(new HighValue(15.0))))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var range = root
            .GetProperty("component")
            .EnumerateArray().First()
            .GetProperty("valueRange");

        Assert.Equal(10.0, range.GetProperty("low").GetProperty("value").GetDouble());
        Assert.Equal(15.0, range.GetProperty("high").GetProperty("value").GetDouble());
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
                    "valueRange" : {
                        "low" : {
                            "value" : 10.0
                        },
                        "high" : {
                            "value" : 15.0
                        }
                    }
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(10.0, observation?.Components.First().Value.Low.Value.Value);
        Assert.Equal(15.0, observation?.Components.First().Value.High.Value.Value);
    }
}
