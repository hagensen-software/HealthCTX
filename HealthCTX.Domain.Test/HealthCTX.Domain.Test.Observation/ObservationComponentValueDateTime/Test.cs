using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValueDateTime;

public class Test
{
    private readonly DateTimeOffset testDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string testDateString = "2024-02-14T13:42:00+01:00";

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
                new ValueDateTime(testDate))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var value = root
            .GetProperty("component")
            .EnumerateArray().First()
            .GetProperty("valueDateTime");

        Assert.Equal(testDateString, value.GetString());
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
                "component" : [{
                    "code" : {
                        "coding" : [{
                            "code" : "8480-6",
                            "system" : "http://loinc.org"
                        }]
                    },
                    "valueDateTime" : "{{testDateString}}"
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testDate, observation?.Components.First().Value.Value);
    }
}
