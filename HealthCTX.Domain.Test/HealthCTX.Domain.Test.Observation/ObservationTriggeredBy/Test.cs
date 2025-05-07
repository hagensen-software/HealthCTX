using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Test.Observation.ObservationInstantiatesCanonical;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationTriggeredBy;

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
            [new TriggeredBy(
                new TriggeredByObservation(
                    new ObservationReference("Observation/123")),
                new TriggeredByType("re-run"),
                new TriggeredByReason("Observation was indecisive"))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var triggeredBy = root.GetProperty("triggeredBy").EnumerateArray().First();

        Assert.Equal("Observation/123", triggeredBy.GetProperty("observation").GetProperty("reference").GetString());
        Assert.Equal("re-run", triggeredBy.GetProperty("type").GetString());
        Assert.Equal("Observation was indecisive", triggeredBy.GetProperty("reason").GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType":"Observation",
                "status":"final",
                "triggeredBy":[{
                    "observation":{
                        "reference":"Observation/123"
                    },
                    "type":"re-run",
                    "reason":"Observation was indecisive"
                }],
                "code":{
                    "coding":[{
                        "code":"8310-5",
                        "system":"http://loinc.org"
                    }],
                    "text":"Observation Code"
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Observation/123", observation?.TriggeredBy.First().Observation.Reference.Value);
        Assert.Equal("re-run", observation?.TriggeredBy.First().Type.Value);
        Assert.Equal("Observation was indecisive", observation?.TriggeredBy.First().Reason?.Value);
    }
}
