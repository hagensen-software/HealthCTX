using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRolePractitioner;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            new PractitionerRolePractitioner(
                new PractitionerReference("Practitioner/123")));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var practitioner = root.GetProperty("practitioner");
        var reference = practitioner.GetProperty("reference");

        Assert.Equal("Practitioner/123", reference.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "practitioner" : {
                    "reference" : "Practitioner/123"
                }
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Practitioner/123", practitionerRole?.Practitioner.Reference.Value);
    }
}
