using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerTelecom;

public class Test
{
    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitioner = new Practitioner(
            new PractitionerTelecom(
                new PractitionerTelecomSystem("phone"),
                new PractitionerTelecomValue("+4555555555")));

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJson(practitioner);

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
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Practitioner",
                "telecom" : [{
                    "system" : "phone",
                    "value" : "+4555555555"
                }]
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("phone", practitioner?.Telecom.System.Value);
        Assert.Equal("+4555555555", practitioner?.Telecom.Value.Value);
    }
}
