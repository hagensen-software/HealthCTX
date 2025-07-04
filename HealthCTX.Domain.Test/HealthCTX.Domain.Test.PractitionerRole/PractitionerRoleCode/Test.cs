using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleCode;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleCode(
                new PractitionerRoleCoding(
                    new RoleCode("admin"),
                    new RoleSystem(new Uri("http://some-role-system"))))]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var types = root.GetProperty("code");
        var type = types.EnumerateArray().GetEnumerator().First();
        var codings = type.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("admin", code.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "code" : [{
                    "coding" : [{
                        "system" : "http://some-role-system",
                        "code" : "admin"
                    }]
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("admin", practitionerRole?.Codes.First().Coding.Code.Value);
    }
}
