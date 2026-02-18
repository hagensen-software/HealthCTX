using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Encounter.EncounterBusinessStatusR6;

public class Test
{
    [Fact]
    public void Encounter_ToFhirJsonR6GeneratesJsonString()
    {
        DateTimeOffset effectiveDate = new(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);
        var encounter = new Encounter(
            new Status("in-progress"),
            [new EncounterBusinessStatus(
                new BusinessStatusCode([new BusinessStatusCodeCoding(new BusinessStatusCodeCodingCode("arrived"))]),
                new BusinessStatusType([new BusinessStatusTypeCoding(new BusinessStatusTypeCodingCode("admission"))]),
                new BusinessStatusEffectiveDate(effectiveDate))]);

        (var jsonString, var outcomes) = EncounterFhirJsonMapper.ToFhirJsonString(encounter, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var businessStatus = root
            .GetProperty("businessStatus")
            .EnumerateArray().First();
        var code = businessStatus
            .GetProperty("code")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");
        var type = businessStatus
            .GetProperty("type")
            .GetProperty("coding")
            .EnumerateArray().First()
            .GetProperty("code");
        var effectiveDateJson = businessStatus.GetProperty("effectiveDate");

        Assert.Equal("arrived", code.GetString());
        Assert.Equal("admission", type.GetString());
        Assert.Equal("2024-01-15T10:30:00+00:00", effectiveDateJson.GetString());
    }

    [Fact]
    public void Encounter_FromFhirJsonR6GeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Encounter",
                "status" : "in-progress",
                "businessStatus": [{
                    "code": {
                        "coding": [{
                            "code": "arrived"
                        }]
                    },
                    "type": {
                        "coding": [{
                            "code": "admission"
                        }]
                    },
                    "effectiveDate": "2024-01-15T10:30:00+00:00"
                }]
            }
            """;

        (var encounter, var outcomes) = EncounterFhirJsonMapper.ToEncounter(jsonString, FhirVersion.R6);

        Assert.Empty(outcomes.Issues);
        Assert.NotNull(encounter);
        Assert.Equal("in-progress", encounter.Status.Value);
        Assert.Single(encounter.BusinessStatuses);
        Assert.Equal("arrived", encounter.BusinessStatuses.First().Code.Codings.First().Code.Value);
        Assert.Equal("admission", encounter.BusinessStatuses.First().Type?.Codings.First().Code.Value);
        Assert.Equal(new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero), encounter.BusinessStatuses.First().EffectiveDate?.Value);
    }
}
