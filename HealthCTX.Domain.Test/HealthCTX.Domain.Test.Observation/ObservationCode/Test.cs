using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationCode;

public class Test
{
    [Fact]
    public void Observation_ToFhirJsonGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var observationCode = root.GetProperty("code");
        var codings = observationCode.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("8310-5", code.GetString());
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
                }
            }
            """;

        (var Observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("8310-5", Observation?.Code.Coding.Code.Value);
    }
}
