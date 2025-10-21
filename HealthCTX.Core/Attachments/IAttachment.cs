using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Attachments;

/// <summary>
/// <para>Interface for the HL7 FHIR Identifier element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>contentType</term>
///         <description><see cref="IAttachmentContentType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>language</term>
///         <description><see cref="IAttachmentLanguage"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>data</term>
///         <description><see cref="IAttachmentData"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>url</term>
///         <description><see cref="IAttachmentUrl"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>size</term>
///         <description><see cref="IAttachmentSize"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>hash</term>
///         <description><see cref="IAttachmentHash"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>title</term>
///         <description><see cref="IAttachmentTitle"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>creation</term>
///         <description><see cref="IAttachmentCreation"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>height</term>
///         <description><see cref="IAttachmentHeight"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>width</term>
///         <description><see cref="IAttachmentWidth"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>frames</term>
///         <description><see cref="IAttachmentFrames"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>duration</term>
///         <description><see cref="IAttachmentDuration"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>pages</term>
///         <description><see cref="IAttachmentPages"/> (HL7 FHIR R5 only)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("contentType", typeof(IAttachmentContentType), Cardinality.Optional)]
[FhirProperty("language", typeof(IAttachmentLanguage), Cardinality.Optional)]
[FhirProperty("data", typeof(IAttachmentData), Cardinality.Optional)]
[FhirProperty("url", typeof(IAttachmentUrl), Cardinality.Optional)]
[FhirProperty("size", typeof(IAttachmentSize), Cardinality.Optional)]
[FhirProperty("hash", typeof(IAttachmentHash), Cardinality.Optional)]
[FhirProperty("title", typeof(IAttachmentTitle), Cardinality.Optional)]
[FhirProperty("creation", typeof(IAttachmentCreation), Cardinality.Optional)]
[FhirProperty("height", typeof(IAttachmentHeight), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("width", typeof(IAttachmentWidth), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("frames", typeof(IAttachmentFrames), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("duration", typeof(IAttachmentDuration), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("pages", typeof(IAttachmentPages), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IAttachment : IElement;
