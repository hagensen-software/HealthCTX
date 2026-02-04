using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationBodyStructureR6;

public class Test
{
    [Fact]
    public void Observation_ToFhirJsonR6WithConceptGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode([new ObservationCodeCoding(
                new CodeValue("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))]),
            new ObservationBodyStructureR6(
                new BodyStructureConcept([new BodyStructureConceptCoding(new BodyStructureConceptCodingCode("left-arm"))]),
                null));

        (var jsonString, var outcomes) = ObservationFhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var bodyStructure = root.GetProperty("bodyStructure");
        var conceptCode = bodyStructure
            .GetProperty("concept")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("left-arm", conceptCode.GetString());
    }

    [Fact]
    public void Observation_ToFhirJsonR6WithReferenceGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode([new ObservationCodeCoding(
                new CodeValue("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))]),
            new ObservationBodyStructureR6(
                null,
                new BodyStructureReference(new BodyStructureReferenceValue("BodyStructure/123"))));

        (var jsonString, var outcomes) = ObservationFhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var bodyStructure = root.GetProperty("bodyStructure");
        var reference = bodyStructure
            .GetProperty("reference")
            .GetProperty("reference");

        Assert.Equal("BodyStructure/123", reference.GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonR6WithConceptGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "code" : {
                    "coding" : [{
                        "code" : "8310-5",
                        "system" : "http://loinc.org"
                    }]
                },
                "bodyStructure" : {
                    "concept" : {
                        "coding" : [{
                            "code" : "left-arm"
                        }]
                    }
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(observation);
        Assert.Equal("left-arm", observation.BodyStructure?.Concept?.Codings.First().Code.Value);
    }

    [Fact]
    public void Observation_FromFhirJsonR6WithReferenceGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "code" : {
                    "coding" : [{
                        "code" : "8310-5",
                        "system" : "http://loinc.org"
                    }]
                },
                "bodyStructure" : {
                    "reference" : {
                        "reference" : "BodyStructure/123"
                    }
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(observation);
        Assert.Equal("BodyStructure/123", observation.BodyStructure?.Reference?.Reference.Value);
    }
}
