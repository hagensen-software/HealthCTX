using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundle;

/// <summary>
/// <para>Interface for the HL7 FHIR Bundle entry element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>link</term>
///         <description><see cref="IBundleLink"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>fullUrl</term>
///         <description><see cref="IBundleEntryFullUrl"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>resource</term>
///         <description><see cref="IBundleEntryResource"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>search</term>
///         <description><see cref="IBundleEntrySearch"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>request</term>
///         <description><see cref="IBundleEntryRequest"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>response</term>
///         <description><see cref="IBundleEntryResponse"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("link", typeof(IBundleLink), Cardinality.Multiple)]
[FhirProperty("fullUrl", typeof(IBundleEntryFullUrl), Cardinality.Optional)]
[FhirProperty("resource", typeof(IBundleEntryResource), Cardinality.Optional)]
[FhirProperty("search", typeof(IBundleEntrySearch), Cardinality.Optional)]
[FhirProperty("request", typeof(IBundleEntryRequest), Cardinality.Optional)]
[FhirProperty("response", typeof(IBundleEntryResponse), Cardinality.Optional)]
public interface IBundleEntry : IElement;
