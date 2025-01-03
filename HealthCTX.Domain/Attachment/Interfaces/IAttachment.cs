using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Attachment.Interfaces;

[FhirElement]
[FhirProperty("contentType", typeof(IAttachmentContentType), Cardinality.Single)]
[FhirProperty("language", typeof(IAttachmentLanguage), Cardinality.Single)]
[FhirProperty("data", typeof(IAttachmentData), Cardinality.Single)]
[FhirProperty("url", typeof(IAttachmentUrl), Cardinality.Single)]
[FhirProperty("size", typeof(IAttachmentSize), Cardinality.Single)]
[FhirProperty("hash", typeof(IAttachmentHash), Cardinality.Single)]
[FhirProperty("title", typeof(IAttachmentTitle), Cardinality.Single)]
[FhirProperty("creation", typeof(IAttachmentCreation), Cardinality.Single)]
public interface IAttachment : IElement;
