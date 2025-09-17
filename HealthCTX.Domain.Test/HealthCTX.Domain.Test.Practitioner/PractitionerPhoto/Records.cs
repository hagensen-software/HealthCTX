using HealthCTX.Domain.Attachments;
using HealthCTX.Domain.Practitioners;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerPhoto;

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
    AttachmentCreation Creation) : IPractitionerPhoto;

public record Practitioner(ImmutableList<PhotoAttachment> Photos) : IPractitioner;
