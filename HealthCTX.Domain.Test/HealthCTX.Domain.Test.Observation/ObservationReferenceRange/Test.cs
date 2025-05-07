using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationReferenceRange;

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
            [new ReferenceRange(
                new ReferenceRangeLow(new LowValue(10.0)),
                new ReferenceRangeHigh(new HighValue(15.0)))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var referenceRange = root.GetProperty("referenceRange").EnumerateArray().First();
        var valueLow = referenceRange.GetProperty("low").GetProperty("value");
        var valueHigh = referenceRange.GetProperty("high").GetProperty("value");

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
                "referenceRange" : [{
                    "low" : {
                        "value" : 10.0
                    },
                    "high" : {
                        "value" : 15.0
                    }
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(10.0, observation?.ReferenceRanges.First().Low.Value.Value);
        Assert.Equal(15.0, observation?.ReferenceRanges.First().High.Value.Value);
    }
}
