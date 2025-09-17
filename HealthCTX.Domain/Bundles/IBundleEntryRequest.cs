using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundles;

/// <summary>
/// <para>Interface for the HL7 FHIR Bundle entry.request elements.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>method</term>
///         <description><see cref="IBundleEntryRequestMethod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>url</term>
///         <description><see cref="IBundleEntryRequestUrl"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>ifNoneMatch</term>
///         <description><see cref="IBundleEntryRequestIfNoneMatch"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>ifModifiedSince</term>
///         <description><see cref="IBundleEntryRequestIfModifiedSince"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>ifMatch</term>
///         <description><see cref="IBundleEntryRequestIfMatch"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>ifNoneExist</term>
///         <description><see cref="IBundleEntryRequestIfNoneExist"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("method", typeof(IBundleEntryRequestMethod), Cardinality.Mandatory)]
[FhirProperty("url", typeof(IBundleEntryRequestUrl), Cardinality.Mandatory)]
[FhirProperty("ifNoneMatch", typeof(IBundleEntryRequestIfNoneMatch), Cardinality.Optional)]
[FhirProperty("ifModifiedSince", typeof(IBundleEntryRequestIfModifiedSince), Cardinality.Optional)]
[FhirProperty("ifMatch", typeof(IBundleEntryRequestIfMatch), Cardinality.Optional)]
[FhirProperty("ifNoneExist", typeof(IBundleEntryRequestIfNoneExist), Cardinality.Optional)]
public interface IBundleEntryRequest : IElement;
