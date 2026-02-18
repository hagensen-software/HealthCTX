using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationInterpretationContextR6;

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
            [new ObservationInterpretationContext(
                new InterpretationContextConcept([new InterpretationContextConceptCoding(new InterpretationContextConceptCodingCode("pregnancy"))]),
                null)]);

        (var jsonString, var outcomes) = ObservationFhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var interpretationContext = root
            .GetProperty("interpretationContext")
            .EnumerateArray().First();
        var conceptCode = interpretationContext
            .GetProperty("concept")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("pregnancy", conceptCode.GetString());
    }

    [Fact]
    public void Observation_ToFhirJsonR6WithReferenceGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode([new ObservationCodeCoding(
                new CodeValue("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))]),
            [new ObservationInterpretationContext(
                null,
                new InterpretationContextReference(new InterpretationContextReferenceValue("Condition/123")))]);

        (var jsonString, var outcomes) = ObservationFhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var interpretationContext = root
            .GetProperty("interpretationContext")
            .EnumerateArray().First();
        var reference = interpretationContext
            .GetProperty("reference")
            .GetProperty("reference");

        Assert.Equal("Condition/123", reference.GetString());
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
                "interpretationContext" : [{
                    "concept" : {
                        "coding" : [{
                            "code" : "pregnancy"
                        }]
                    }
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(observation);
        Assert.Single(observation.InterpretationContexts);
        Assert.Equal("pregnancy", observation.InterpretationContexts.First().Concept?.Codings.First().Code.Value);
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
                "interpretationContext" : [{
                    "reference" : {
                        "reference" : "Condition/123"
                    }
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(observation);
        Assert.Single(observation.InterpretationContexts);
        Assert.Equal("Condition/123", observation.InterpretationContexts.First().Reference?.Reference.Value);
    }
}
