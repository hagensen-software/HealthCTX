using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Persons.PersonMaritalStatus;

public class Test
{
    [Fact]
    public void Person_ToFhirJsonGeneratesJsonString()
    {
        var person = new Person(
            new PersonMaritalStatus(
                new PersonMaritalStatusCoding(
                    new PersonMaritalStatusCode("M"),
                    new PersonMaritalStatusSystem(new Uri("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus")),
                    new PersonMaritalStatusDisplay("Married")),
                new PersonMaritalStatusText("Married")));

        (var jsonString, _) = PersonFhirJsonMapper.ToFhirJsonString(person, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var maritalStatus = root.GetProperty("maritalStatus");
        var codings = maritalStatus.GetProperty("coding");
        var coding = codings.EnumerateArray().GetEnumerator().First();
        var code = coding.GetProperty("code");
        var text = maritalStatus.GetProperty("text");

        Assert.Equal("M", code.GetString());
        Assert.Equal("Married", text.GetString());
    }

    [Fact]
    public void Person_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Person",
                "maritalStatus" : {
                    "coding" : [{
                        "system" : "http://terminology.hl7.org/CodeSystem/v3-MaritalStatus",
                        "code" : "M",
                        "display" : "Married"
                    }],
                    "text" : "Married"
                }
            }
            """;

        (var person, var outcomes) = PersonFhirJsonMapper.ToPerson(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("M", person?.MaritalStatus.Coding.Code.Value);
        Assert.Equal("Married", person?.MaritalStatus.Coding.Display.Value);
    }
}
