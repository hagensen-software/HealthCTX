using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerName;

public class Test
{
    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitioner = new Practitioner(
                new PractitionerName(
                    new PractitionerFamilyName("Doe"),
                    [new PractitionerGivenName("John"), new PractitionerGivenName("D")]));

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJson(practitioner);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var names = root.GetProperty("name");
        var name = names.EnumerateArray().GetEnumerator().First();
        var family = name.GetProperty("family");
        var givens = name.GetProperty("given");
        var given = givens.EnumerateArray().GetEnumerator().First();

        Assert.Equal("Doe", family.GetString());
        Assert.Equal("John", given.GetString());
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Practitioner",
                "name" : [{
                    "family" : "Doe",
                    "given" : ["John", "D"]
                }]
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Doe", practitioner?.Name.Family.Value);
        Assert.Equal("John", practitioner?.Name.Given.First().Value);
    }
}
