using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleSpecialty;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleCharacteristic;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleCharacteristic(
                new PractitionerRoleCharacteristicCoding(
                    new CharacteristicCode("telephone"),
                    new CharacteristicSystem(new Uri("http://hl7.org/fhir/service-mode"))))]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var specialties = root.GetProperty("characteristic");
        var specialty = specialties.EnumerateArray().GetEnumerator().First();
        var codings = specialty.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("telephone", code.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "characteristic" : [{
                    "coding" : [{
                        "system" : "http://hl7.org/fhir/service-mode",
                        "code" : "telephone"
                    }]
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("telephone", practitionerRole?.Characteristics.First().Coding.Code.Value);
    }
}
