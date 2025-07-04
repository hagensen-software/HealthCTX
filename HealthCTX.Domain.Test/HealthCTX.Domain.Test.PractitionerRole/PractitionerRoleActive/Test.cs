using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleActive;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            new PractitionerRoleActive(true));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var active = root.GetProperty("active");

        Assert.True(active.GetBoolean());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "active" : true
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.True(practitionerRole?.Active.Value);
    }
}
