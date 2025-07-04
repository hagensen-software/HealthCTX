using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationBodySite;

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
            new ObservationBodySite(
                new BodySiteCoding(
                    new BodySiteCode("344001"),
                    new BodySiteSystem(new Uri("http://snomed.info/sct")))));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJsonString(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var bodySite = root.GetProperty("bodySite");
        var codings = bodySite.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("344001", code.GetString());
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
                "bodySite" : {
                    "coding" : [{
                        "code" : "344001",
                        "system" : "http://snomed.info/sct"
                    }]
                }
            }
            """;

        (var Observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("344001", Observation?.BodySite?.Coding.Code.Value);
    }
}
