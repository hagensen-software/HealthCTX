using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundles;

/// <summary>
///     <para>Interface for the HL7 FHIR Bundle resource.</para>
///     <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
///     <list type="table">
///         <item>
///             <term>identifier</term>
///             <description><see cref="IBundleIdentifier"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>type</term>
///             <description><see cref="IBundleType"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>timestamp</term>
///             <description><see cref="IBundleTimestamp"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>total</term>
///             <description><see cref="IBundleTotal"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>link</term>
///             <description><see cref="IBundleLink"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>entry</term>
///             <description><see cref="IBundleEntry"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>signature</term>
///             <description><see cref="IBundleSignature"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>issues</term>
///             <description><see cref="IBundleIssues"/> (HL7 FHIR R5 only)</description>
///         </item>
///     </list>
/// </summary>
[FhirResource("Bundle")]
[FhirProperty("identifier", typeof(IBundleIdentifier), Cardinality.Optional)]
[FhirProperty("type", typeof(IBundleType), Cardinality.Mandatory)]
[FhirProperty("timestamp", typeof(IBundleTimestamp), Cardinality.Optional)]
[FhirProperty("total", typeof(IBundleTotal), Cardinality.Optional)]
[FhirProperty("link", typeof(IBundleLink), Cardinality.Multiple)]
[FhirProperty("entry", typeof(IBundleEntry), Cardinality.Multiple)]
[FhirProperty("signature", typeof(IBundleSignature), Cardinality.Optional)]
[FhirProperty("issues", typeof(IBundleIssues), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IBundle : IResource;
