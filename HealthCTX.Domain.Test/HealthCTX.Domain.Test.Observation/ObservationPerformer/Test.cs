using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationPerformer;

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
            [new ObservationPerformer(
                new ObservationPerformerReference("Practitioner/123"))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var performer = root.GetProperty("performer").EnumerateArray().First();
        var reference = performer.GetProperty("reference");

        Assert.Equal("Practitioner/123", reference.GetString());
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
                "performer" : [{
                    "reference" : "Practitioner/123"
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Practitioner/123", observation?.Performer?.First().Reference.Value);
    }
}
