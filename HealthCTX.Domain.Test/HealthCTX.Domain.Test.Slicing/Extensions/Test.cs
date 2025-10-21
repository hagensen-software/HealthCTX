using System.Text.Json;

namespace HealthCTX.Domain.Test.Slicing.Extensions;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new OfficialName(
                new FamilyName("Doe"),
                new MiddleName(
                    new MiddleNameText("M")),
                new GivenName("John")));

        (var jsonString, _) = patient.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);

        var name = document.RootElement
            .GetProperty("name")
            .EnumerateArray().First();

        var family = name.GetProperty("family").GetString();
        var middle = name.GetProperty("extension")
            .EnumerateArray().First()
            .GetProperty("valueString")
            .GetString();
        var given = name.GetProperty("given").EnumerateArray().First().GetString();

        Assert.Equal("Doe", family);
        Assert.Equal("M", middle);
        Assert.Equal("John", given);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType": "Patient",
                "name": [{
                    "extension": [{
                        "url":"http://example.org/fhir/StructureDefinition/humanname-middle",
                        "valueString":"M"
                    }],
                    "family":"Doe",
                    "given":[
                        "John"
                    ]
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Doe", patient?.Name.FamilyName.Value);
        Assert.Equal("M", patient?.Name.MiddleName.Text.Value);
        Assert.Equal("John", patient?.Name.GivenName.Value);
    }
}
