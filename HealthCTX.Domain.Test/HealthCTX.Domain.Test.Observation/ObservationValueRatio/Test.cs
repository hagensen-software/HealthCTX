using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationValueRatio;

public class Test
{
    [Fact]
    public void Observation_ToFhirJsonGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))),
            new ValueRatio(
                new RatioNumerator(new Numerator(2.0)),
                new RatioDenominator(new Denominator(3.0))));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var valueLow = root.GetProperty("valueRatio").GetProperty("numerator").GetProperty("value");
        var valueHigh = root.GetProperty("valueRatio").GetProperty("denominator").GetProperty("value");

        Assert.Equal(2.0, valueLow.GetDouble());
        Assert.Equal(3.0, valueHigh.GetDouble());
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
                "valueRatio" : {
                    "numerator" : {
                        "value" : 2.0
                    },
                    "denominator" : {
                        "value" : 3.0
                    }
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(2.0, observation?.Ratio.Numerator.Value.Value);
        Assert.Equal(3.0, observation?.Ratio.Denominator.Value.Value);
    }
}
