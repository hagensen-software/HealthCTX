using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentInterpretation;

public class Test
{
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
                [new ObservationInterpretation(
                    new InterpretationCoding(
                        new InterpretationCode("B"),
                        new InterpretationSystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation"))))])]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJsonString(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var interpretation = root
            .GetProperty("component")
            .EnumerateArray().First()
            .GetProperty("interpretation")
            .EnumerateArray().First()
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("B", interpretation.GetString());
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
                "component" : [{
                    "code" : {
                        "coding" : [{
                            "code" : "8480-6",
                            "system" : "http://loinc.org"
                        }]
                    },
                    "interpretation" : [{
                        "coding" : [{
                            "code" : "B",
                            "system" : "http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation"
                        }]
                    }]
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("B", observation?.Components.First().Interpretations.First().Coding.Code.Value);
    }
}
