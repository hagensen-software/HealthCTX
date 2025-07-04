using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleIdentifier;

public class Test
{
    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
                new PractitionerRoleIdentifier(
                    new PractitionerRoleIdentifierSystem(new Uri("http://someroleidentifier-system")),
                    new PractitionerRoleIdentifierValue("admin")));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var identifiers = root.GetProperty("identifier");
        var identifier = identifiers.EnumerateArray().GetEnumerator().First();
        var system = identifier.GetProperty("system");
        var value = identifier.GetProperty("value");

        Assert.Equal("http://someroleidentifier-system", system.GetString());
        Assert.Equal("admin", value.GetString());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "PractitionerRole",
                "identifier" : [{
                    "system" : "http://someroleidentifier-system",
                    "value" : "admin"
                }]
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("http://someroleidentifier-system", practitionerRole?.Identifier.System.Value.OriginalString);
        Assert.Equal("admin", practitionerRole?.Identifier.Value.Value);
    }
}
