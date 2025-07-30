using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentDataAbsentReason;

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
                new DataAbsentReason(
                    new ReasonCoding(
                        new ReasonCode("not-performed"),
                        new ReasonSystem(new Uri("http://terminology.hl7.org/CodeSystem/data-absent-reason")))))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJsonString(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var reason = root
            .GetProperty("component")
            .EnumerateArray().First()
            .GetProperty("dataAbsentReason")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");

        Assert.Equal("not-performed", reason.GetString());
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
                    "dataAbsentReason" : {
                        "coding" : [{
                            "code" : "not-performed",
                            "system" : "http://terminology.hl7.org/CodeSystem/data-absent-reason"
                        }]
                    }
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("not-performed", observation?.Components.First().DataAbsentReason.Coding.Code.Value);
    }
}
