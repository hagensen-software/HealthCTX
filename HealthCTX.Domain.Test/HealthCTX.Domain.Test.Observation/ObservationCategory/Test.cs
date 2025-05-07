using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationCategory;

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
            [new ObservationCategory(
                new ObservationCategoryCoding(
                    new ObservationCategoryCode("vital-signs"),
                    new ObservationCategorySystem(new Uri("http://terminology.hl7.org/CodeSystem/observation-category"))))]);

        (var jsonString, _) = ObservationFhirJsonMapper.ToFhirJson(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var categories = root.GetProperty("category");
        var category = categories.EnumerateArray().GetEnumerator().First();
        var codings = category.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("vital-signs", code.GetString());
    }

    [Fact]
    public void Observation_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "category" : [{
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/observation-category",
                        "code" : "vital-signs"
                    }]
                }],
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
        Assert.Equal("vital-signs", Observation?.Categories.First().Coding.Code.Value);
    }
}
