using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerGender;

public class Test
{
    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitioner = new Practitioner(
            new PractitionerGender("male"));

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJsonString(practitioner);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var gender = root.GetProperty("gender");

        Assert.Equal("male", gender.GetString());
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Practitioner",
                "gender" : "male"
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("male", practitioner?.Gender.Value);
    }
}
