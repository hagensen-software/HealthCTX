using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationDataAbsentReason;

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
            new DataAbsentReason(
                new ReasonCoding(
                    new ReasonCode("not-performed"),
                    new ReasonSystem(new Uri("http://terminology.hl7.org/CodeSystem/data-absent-reason")))));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var dataAbsentReason = root.GetProperty("dataAbsentReason");
        var codings = dataAbsentReason.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("not-performed", code.GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "dataAbsentReason" : {
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/data-absent-reason",
                        "code" : "not-performed"
                    }]
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

        (var Observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("not-performed", Observation?.DataAbsentReason?.Coding.Code.Value);
    }
}
