using HealthCTX.Domain.Test.Observation.ObservationValueString;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationValueTime;

public class Test
{
    private readonly TimeOnly testTime = new(13, 42, 0);
    private readonly string testTimeString = "13:42:00";

    [Fact]
    public void Observation_ToFhirJsonGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))),
            new ValueTime(testTime));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var value = root.GetProperty("valueTime");

        Assert.Equal(testTimeString, value.GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
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
                "valueTime" : "{{testTimeString}}"
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testTime, observation?.Value.Value);
    }
}
