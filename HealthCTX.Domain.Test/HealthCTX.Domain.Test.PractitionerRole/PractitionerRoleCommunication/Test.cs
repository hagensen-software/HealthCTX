using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleCommunication;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            [new PractitionerRoleCommunication(
                new PractitionerRoleCommunicationCoding(
                    new CommunicationCode("da-DK"),
                    new CommunicationSystem(new Uri("urn:ietf:bcp:47"))))]);

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJson(practitionerRole, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var specialties = root.GetProperty("communication");
        var specialty = specialties.EnumerateArray().GetEnumerator().First();
        var codings = specialty.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");

        Assert.Equal("da-DK", code.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "communication" : [{
                    "coding" : [{
                        "system" : "urn:ietf:bcp:47",
                        "code" : "da-DK"
                    }]
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("da-DK", practitionerRole?.Communications.First().Coding.Code.Value);
    }
}
