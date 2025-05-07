using HealthCTX.Domain.Test.Observation.ObservationDataAbsentReason;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationMethod;

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
            new ObservationMethod(
                new MethodCoding(
                    new MethodCode("258083009"),
                    new MethodSystem(new Uri("http://snomed.info/sct")))));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var method = root.GetProperty("method");
        var codings = method.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("258083009", code.GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "method" : {
                    "coding" : [{
                        "system" : "http://snomed.info/sct",
                        "code" : "258083009"
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
        Assert.Equal("258083009", Observation?.Method?.Coding.Code.Value);
    }
}
