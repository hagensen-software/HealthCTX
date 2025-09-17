using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundles;

/// <summary>
/// <para>Interface for the HL7 FHIR Bundle entry.response elements.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>status</term>
///         <description><see cref="IBundleEntryResponseStatus"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>location</term>
///         <description><see cref="IBundleEntryResponseLocation"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>etag</term>
///         <description><see cref="IBundleEntryResponseEtag"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>lastModified</term>
///         <description><see cref="IBundleEntryResponseLastModified"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>outcome</term>
///         <description><see cref="IBundleEntryResponseOutcome"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("status", typeof(IBundleEntryResponseStatus), Cardinality.Mandatory)]
[FhirProperty("location", typeof(IBundleEntryResponseLocation), Cardinality.Optional)]
[FhirProperty("etag", typeof(IBundleEntryResponseEtag), Cardinality.Optional)]
[FhirProperty("lastModified", typeof(IBundleEntryResponseLastModified), Cardinality.Optional)]
[FhirProperty("outcome", typeof(IBundleEntryResponseOutcome), Cardinality.Optional)]
public interface IBundleEntryResponse : IElement;
