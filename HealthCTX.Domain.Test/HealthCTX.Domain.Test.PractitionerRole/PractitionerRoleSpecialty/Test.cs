using HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleCode;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleSpecialty;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleSpecialty(
                new PractitionerRoleSpecialtyCoding(
                    new SpecialtyCode("internal medicine"),
                    new SpecialtySystem(new Uri("http://some-role-system"))))]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var specialties = root.GetProperty("specialty");
        var specialty = specialties.EnumerateArray().GetEnumerator().First();
        var codings = specialty.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("internal medicine", code.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "specialty" : [{
                    "coding" : [{
                        "system" : "http://some-role-system",
                        "code" : "internal medicine"
                    }]
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("internal medicine", practitionerRole?.Specialties.First().Coding.Code.Value);
    }
}
