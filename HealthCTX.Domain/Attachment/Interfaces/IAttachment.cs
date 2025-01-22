using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Attachment.Interfaces;

[FhirElement]
[FhirProperty("contentType", typeof(IAttachmentContentType), Cardinality.Optional)]
[FhirProperty("language", typeof(IAttachmentLanguage), Cardinality.Optional)]
[FhirProperty("data", typeof(IAttachmentData), Cardinality.Optional)]
[FhirProperty("url", typeof(IAttachmentUrl), Cardinality.Optional)]
[FhirProperty("size", typeof(IAttachmentSize), Cardinality.Optional)]
[FhirProperty("hash", typeof(IAttachmentHash), Cardinality.Optional)]
[FhirProperty("title", typeof(IAttachmentTitle), Cardinality.Optional)]
[FhirProperty("creation", typeof(IAttachmentCreation), Cardinality.Optional)]
public interface IAttachment : IElement;
