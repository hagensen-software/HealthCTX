using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerActive;

public class Test
{
    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitioner = new Practitioner(
            new PractitionerActive(true));

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJsonString(practitioner);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var active = root.GetProperty("active");

        Assert.True(active.GetBoolean());
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Practitioner",
                "active" : true
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.True(practitioner?.Active.Value);
    }
}
