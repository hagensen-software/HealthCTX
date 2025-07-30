using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValueRatio;

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
            new ValueRatio(
                new RatioNumerator(new Numerator(2.0)),
                new RatioDenominator(new Denominator(3.0))))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJsonString(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var range = root
            .GetProperty("component")
            .EnumerateArray().First()
            .GetProperty("valueRatio");

        Assert.Equal(2.0, range.GetProperty("numerator").GetProperty("value").GetDouble());
        Assert.Equal(3.0, range.GetProperty("denominator").GetProperty("value").GetDouble());
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
                    "valueRatio" : {
                        "numerator" : {
                            "value" : 2.0
                        },
                        "denominator" : {
                            "value" : 3.0
                        }
                    }
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(2.0, observation?.Components.First().Value.Numerator.Value.Value);
        Assert.Equal(3.0, observation?.Components.First().Value.Denominator.Value.Value);
    }
}
