using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationBodyStructure;

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
            new ObservationBodyStructure(
                new BodyStructureReference("BodyStructure/123")));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var reference = root.GetProperty("bodyStructure").GetProperty("reference");

        Assert.Equal("BodyStructure/123", reference.GetString());
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
                "bodyStructure" : {
                    "reference" : "BodyStructure/123"
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("BodyStructure/123", observation?.BodyStructure?.Reference.Value);
    }
}
