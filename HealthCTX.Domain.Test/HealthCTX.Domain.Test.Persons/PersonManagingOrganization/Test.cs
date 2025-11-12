using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonManagingOrganization;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
            new PersonManagingOrganization(
                new PersonManagingOrganizationReference("Organization/123")));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var managingOrganization = root.GetProperty("managingOrganization");
        var reference = managingOrganization.GetProperty("reference");

        Assert.Equal("Organization/123", reference.GetString());
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Person",
                "managingOrganization" : {
                    "reference" : "Organization/123"
                }
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Organization/123", person?.ManagingOrganization.Reference.Value);
    }
}
