using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerBirthDate;

public class Test
{
    private readonly DateOnly testDate = new(2024, 2, 14);
    private readonly string testDateString = "2024-02-14";

    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitionerDeceasedDateTime = new PractitionerBirthDate(testDate);
        var practitioner = new Practitioner(practitionerDeceasedDateTime);

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJson(practitioner);

        var deceased = string.Empty;
        using (var document = JsonDocument.Parse(jsonString!))
        {
            JsonElement root = document.RootElement;
            deceased = root.GetProperty("birthDate").GetString();
        }

        Assert.Equal(testDateString, deceased);
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Practitioner",
                "birthDate" : "{{testDateString}}"
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(testDate, practitioner?.BirthDate.Value);
    }
}
