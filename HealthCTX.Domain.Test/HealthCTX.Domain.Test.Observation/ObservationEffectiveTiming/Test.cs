using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationEffectiveTiming;

public class Test
{
    private readonly DateTimeOffset eventTime = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string eventTimeString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Observation_ToFhirJsonGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))),
            new EffectiveTiming(
                new TimingEvent(eventTime)));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var eventNode = root.GetProperty("effectiveTiming").GetProperty("event").EnumerateArray().First();

        Assert.Equal(eventTimeString, eventNode.GetString());
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
                "effectiveTiming" : {
                    "event" : ["{{eventTimeString}}"]
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(eventTime, observation?.Effective?.Event?.Value);
    }
}
