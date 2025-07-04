using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationNote;

public class Test
{
    private readonly DateTimeOffset testDateTime = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string testDateTimeString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Observation_ToFhirJsonGeneratesJsonString()
    {
        var observation = new Observation(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))),
            [new ObservationNote(
                new Author("Hagensen"),
                new AuthoredTime(testDateTime),
                new Text("Some note"))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJsonString(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var notes = root.GetProperty("note");
        var note = notes.EnumerateArray().First();

        Assert.Equal("Hagensen", note.GetProperty("authorString").GetString());
        Assert.Equal(testDateTimeString, note.GetProperty("time").GetString());
        Assert.Equal("Some note", note.GetProperty("text").GetString());
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
                "note" : [{
                    "authorString" : "Hagensen",
                    "time" : "2024-02-14T13:42:00+01:00",
                    "text" : "Some note"
                }]
            }
            """;

        (var observation, var outcomes) = ObservationFhirJsonMapper.ToObservation(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Hagensen", observation?.Notes.First().Author?.Value);
        Assert.Equal(testDateTime, observation?.Notes.First().Time?.Value);
        Assert.Equal("Some note", observation?.Notes.First().Text.Value);
    }
}
