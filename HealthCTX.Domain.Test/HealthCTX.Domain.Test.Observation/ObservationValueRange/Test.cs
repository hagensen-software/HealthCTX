using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationValueRange;

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
            new ValueRange(
                new ObservationRangeLow(new LowValue(10.0)),
                new ObservationRangeHigh(new HighValue(15.0))));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var valueLow = root.GetProperty("valueRange").GetProperty("low").GetProperty("value");
        var valueHigh = root.GetProperty("valueRange").GetProperty("high").GetProperty("value");

        Assert.Equal(10.0, valueLow.GetDouble());
        Assert.Equal(15.0, valueHigh.GetDouble());
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
                "valueRange" : {
                    "low" : {
                        "value" : 10.0
                    },
                    "high" : {
                        "value" : 15.0
                    }
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(10.0, observation?.Value.Low.Value.Value);
        Assert.Equal(15.0, observation?.Value.High.Value.Value);
    }
}
