using System.Text.Json;

namespace HealthCTX.Domain.Test.Extensions.PatientHumanNames;

public class Test
{
    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            new OfficialName(
                new FamilyName("Doe"),
                new GivenName("John")),
            new Nickname(
                new NameText("The who")));

        (var jsonString, _) = patient.ToFhirJsonString();

        using var document = JsonDocument.Parse(jsonString!);

        var names = document.RootElement
            .GetProperty("name");

        var name = names.EnumerateArray().First();
        var nickname = names.EnumerateArray().Skip(1).First();

        var family = name.GetProperty("family").GetString();
        var given = name.GetProperty("given").EnumerateArray().First().GetString();
        var nicknametext = nickname.GetProperty("text").GetString();

        Assert.Equal("Doe", family);
        Assert.Equal("John", given);
        Assert.Equal("The who", nicknametext);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType": "Patient",
                "name": [
                    {
                        "use": "official",
                        "family":"Doe",
                        "given":[
                            "John"
                        ]
                    },
                    {
                        "use": "nickname",
                        "text":"The who"
                    }
                ]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("Doe", patient?.Name.FamilyName.Value);
        Assert.Equal("John", patient?.Name.GivenName.Value);
        Assert.Equal("The who", patient?.Nickname?.Text.Value);
    }
}
