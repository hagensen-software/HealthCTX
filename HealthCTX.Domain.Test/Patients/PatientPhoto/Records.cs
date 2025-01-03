using HealthCTX.Domain.Attachment.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Patients.PatientPhoto;

public record AttachmentContentType(string Value) : IAttachmentContentType;
public record AttachmentLanguage(string Value) : IAttachmentLanguage;
public record AttachmentData(string Value) : IAttachmentData;
public record AttachmentUrl(Uri Value) : IAttachmentUrl;
public record AttachmentSize(long Value) : IAttachmentSize;
public record AttachmentHash(string Value) : IAttachmentHash;
public record AttachmentTitle(string Value) : IAttachmentTitle;
public record AttachmentCreation(DateTimeOffset Value) : IAttachmentCreation;

public record PhotoAttachment(
    AttachmentContentType ContentType,
    AttachmentLanguage Language,
    AttachmentData Data,
    AttachmentUrl Url,
    AttachmentSize Size,
    AttachmentHash Hash,
    AttachmentTitle Title,
    AttachmentCreation Creation) : IPatientPhoto;

public record Patient(ImmutableList<PhotoAttachment> Photos) : IPatient;
