using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Attachment;

[FhirElement]
[FhirProperty("contentType", typeof(IAttachmentContentType), Cardinality.Optional)]
[FhirProperty("language", typeof(IAttachmentLanguage), Cardinality.Optional)]
[FhirProperty("data", typeof(IAttachmentData), Cardinality.Optional)]
[FhirProperty("url", typeof(IAttachmentUrl), Cardinality.Optional)]
[FhirProperty("size", typeof(IAttachmentSize), Cardinality.Optional)]
[FhirProperty("hash", typeof(IAttachmentHash), Cardinality.Optional)]
[FhirProperty("title", typeof(IAttachmentTitle), Cardinality.Optional)]
[FhirProperty("creation", typeof(IAttachmentCreation), Cardinality.Optional)]
[FhirProperty("height", typeof(IAttachmentHeight), Cardinality.Optional)]
[FhirProperty("width", typeof(IAttachmentWidth), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("frames", typeof(IAttachmentFrames), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("duration", typeof(IAttachmentDuration), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("pages", typeof(IAttachmentPages), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IAttachment : IElement;
