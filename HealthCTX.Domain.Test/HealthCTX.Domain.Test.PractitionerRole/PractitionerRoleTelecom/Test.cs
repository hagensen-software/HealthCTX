using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleTelecom;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            new PractitionerRoleTelecom(
                new PractitionerRoleTelecomSystem("phone"),
                new PractitionerRoleTelecomValue("+4555555555")));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var telecoms = root.GetProperty("telecom");
        var telecom = telecoms.EnumerateArray().GetEnumerator().First();
        var system = telecom.GetProperty("system");
        var value = telecom.GetProperty("value");

        Assert.Equal("phone", system.GetString());
        Assert.Equal("+4555555555", value.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "PractitionerRole",
                "telecom" : [{
                    "system" : "phone",
                    "value" : "+4555555555"
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("phone", practitionerRole?.Telecom.System.Value);
        Assert.Equal("+4555555555", practitionerRole?.Telecom.Value.Value);
    }
}
