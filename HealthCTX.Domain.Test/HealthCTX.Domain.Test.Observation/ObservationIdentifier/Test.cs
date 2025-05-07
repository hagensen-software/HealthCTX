using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationIdentifier;

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
            [new ObservationIdentifier(
                new ObservationIdentifierSystem(new Uri("http://some-observation-identifier-system")),
                new ObservationIdentifierValue("12345678"))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var identifiers = root.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://some-observation-identifier-system", system.GetString());
        Assert.Equal("12345678", value.GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Observation",
                "status" : "final",
                "identifier" : [{
                    "system" : "http://some-observation-identifier-system",
                    "value" : "12345678"
                }],
                "code" : {
                    "coding" : [{
                        "code" : "8310-5",
                        "system" : "http://loinc.org"
                    }],
                    "text" : "Observation Code"
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://some-observation-identifier-system", observation?.Identifiers.First().System.Value.OriginalString);
        Assert.Equal("12345678", observation?.Identifiers.First().Value.Value);
    }
}
