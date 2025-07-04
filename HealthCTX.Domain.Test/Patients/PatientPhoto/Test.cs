using HealthCTX.Domain.Test.Patients.PatientAddress;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Patients.PatientPhoto;

public class Test
{
    private readonly DateTimeOffset creationDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string creationDateString = "2024-02-14T13:42:00+01:00";

    [Fact]
    public void Patient_ToFhirJsonGeneratesJsonString()
    {
        var patient = new Patient(
            [new PhotoAttachment(
                new AttachmentContentType("some mime type"),
                new AttachmentLanguage("some language"),
                new AttachmentData("some base64 encoded data"),
                new AttachmentUrl(new Uri("http://someurl.org")),
                new AttachmentSize(1234),
                new AttachmentHash("some base64 encoded hash value"),
                new AttachmentTitle("some title"),
                new AttachmentCreation(creationDate)
                )]);

        (var jsonString, _) = PatientFhirJsonMapper.ToFhirJsonString(patient);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;
        var photos = root.GetProperty("photo");
        var photo = photos.EnumerateArray().GetEnumerator().First();
        var mimeType = photo.GetProperty("contentType").GetString();
        var creation = photo.GetProperty("creation").GetString();

        Assert.Equal(JsonValueKind.Object, photo.ValueKind);
        Assert.Equal("some mime type", mimeType);
        Assert.Equal(creationDateString, creation);
    }

    [Fact]
    public void Patient_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Patient",
                "photo" : [
                    {
                        "contentType" : "some mime type",
                        "language" : "some language",
                        "data" : "some base64 encoded data",
                        "url" : "http://someurl.org",
                        "size" : 1234,
                        "hash" : "some base64 encoded hash value",
                        "title" : "some title",
                        "creation" : "{{creationDateString}}"
                    }
                ]
            }
            """;

        (var patient, var outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("some mime type", patient?.Photos.First().ContentType.Value);
        Assert.Equal(creationDate, patient?.Photos.First().Creation.Value);
    }
}
