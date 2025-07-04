using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Test.Observation.ObservationValueString;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationValueAttachment;

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
            new Attachment(
                new Data("U29tZSBhdHRhY2htZW50IGRhdGE=")));

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var value = root.GetProperty("valueAttachment").GetProperty("data");

        Assert.Equal("U29tZSBhdHRhY2htZW50IGRhdGE=", value.GetString());
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
                "valueAttachment" : {
                    "data" : "U29tZSBhdHRhY2htZW50IGRhdGE="
                }
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("U29tZSBhdHRhY2htZW50IGRhdGE=", observation?.Value.Data.Value);
    }
}
