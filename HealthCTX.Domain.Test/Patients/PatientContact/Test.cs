using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientContact;

public class Test
{
    private readonly DateTimeOffset periodStartDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string periodStartDateString = "2024-02-14T13:42:00+01:00";
    private readonly DateTimeOffset periodEndDate = new(2024, 1, 31, 0, 0, 0, new TimeSpan(1, 0, 0));
    private readonly string periodEndDateString = "2024-01-31T00:00:00+01:00";

    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            [new PatientContact(
                new PatientContactRelationship(
                    new RelationshipCoding(
                        new RelationshipSystem(new Uri("http://hl7.org/fhir/v2/0131")),
                        new RelationshipCode("N")),
                    new RelationshipText("some text")),
                new PatientContactHumanName(new FamilyName("some family name"), new GivenName("some given name")),
                [new PatientContactContactPoint(new ContactPointSystem("phone"), new ContactPointValue("some value"))],
                new PatientContactAddress(
                    [new PatientContactLine("some line"), new PatientContactLine("another line")],
                    new PatientContactCity("some city"),
                    new PatientContactPostalCode("some postal code")),
                new PatientContactGender("male"),
                new PatientContactOrganization(new PatientContactReference("some organization reference")),
                new PatientContactPeriod(new PeriodStart(DateTimeOffset.MinValue), new PeriodEnd(DateTimeOffset.MaxValue)))]);

        var jsonString = PatientFhirJsonMapper.ToFhirJson(patient);

        using var document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;
        var contacts = root.GetProperty("contact");
        var contact = contacts.EnumerateArray().GetEnumerator().First();
        var relationship = contact.GetProperty("relationship");
        var telecom = contact.GetProperty("telecom");
        var period = contact.GetProperty("period");

        Assert.Equal(JsonValueKind.Object, contact.ValueKind);
        Assert.Equal(JsonValueKind.Array, relationship.ValueKind);
        Assert.Equal(JsonValueKind.Array, telecom.ValueKind);
        Assert.Equal(JsonValueKind.Object, period.ValueKind);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Patient",
                "contact" : [{
                    "relationship" : [
                        {
                            "coding" : [
                                {
                                    "system" : "http://hl7.org/fhir/v2/0131",
                                    "code" : "N"
                                }
                            ],
                            "text" : "some text"
                        }],
                    "name" : {
                        "family" : "some family name",
                        "given" : [ "some given name" ]
                    },
                    "telecom" : [
                        {
                            "system" : "phone",
                            "value" : "some value"
                        }
                    ],
                    "address" : {
                        "line" : [ "some line", "another line" ],
                        "city" : "some city",
                        "postalCode" : "some postal code"
                    },
                    "gender" : "male",
                    "organization" : {
                        "reference" : "some organization reference"
                    },
                    "period" : {
                        "start" : "{{periodStartDateString}}",
                        "end" : "{{periodEndDateString}}"
                    }
                }]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("N", patient?.Contacts.First().Relationship.Coding.Code.Value);
        Assert.Collection(patient?.Contacts.First().Address.Line!,
            l => Assert.Equal("some line", l.Value),
            l => Assert.Equal("another line", l.Value));
        Assert.Equal(periodStartDate, patient?.Contacts.First().Period.Start.Value);
        Assert.Equal(periodEndDate, patient?.Contacts.First().Period.End.Value);
    }
}
