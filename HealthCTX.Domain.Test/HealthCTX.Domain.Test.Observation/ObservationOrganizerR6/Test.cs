using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationOrganizerR6;

public class Test
{
    [Fact]
    public void Observation_ToFhirJsonR6GeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode([new ObservationCodeCoding(
                new CodeValue("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))]),
            new ObservationOrganizer(true));

        (var jsonString, var outcomes) = ObservationFhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.True(root.GetProperty("organizer").GetBoolean());
    }

    [Fact]
    public void Observation_FromFhirJsonR6GeneratesRecords()
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
                "organizer" : true
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(observation);
        Assert.True(observation.Organizer?.Value);
    }

    [Fact]
    public void Observation_ToFhirJsonR6WithOrganizerFalseGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode([new ObservationCodeCoding(
                new CodeValue("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))]),
            new ObservationOrganizer(false));

        (var jsonString, var outcomes) = ObservationFhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        Assert.False(root.GetProperty("organizer").GetBoolean());
    }
}
