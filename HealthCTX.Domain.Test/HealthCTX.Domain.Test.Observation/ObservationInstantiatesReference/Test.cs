using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationInstantiatesReference;

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
            new Instantiates(
                new InstantiatesReference("ObservationDefinition/123")));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var partOf = root.GetProperty("instantiatesReference");
        var reference = partOf.GetProperty("reference");

        Assert.Equal("ObservationDefinition/123", reference.GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "instantiatesReference" : {
                    "reference" : "ObservationDefinition/123"
                },
                "code" : {
                    "coding" : [{
                        "code" : "8310-5",
                        "system" : "http://loinc.org"
                    }],
                    "text" : "Observation Code"
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("ObservationDefinition/123", observation?.Instantiates?.Reference.Value);
    }
}
