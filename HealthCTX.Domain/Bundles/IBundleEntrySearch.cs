using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundles;

/// <summary>
/// <para>Interface for the HL7 FHIR Bundle link and entry.search elements.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>mode</term>
///         <description><see cref="IBundleEntrySearchMode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>score</term>
///         <description><see cref="IBundleEntrySearchScore"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("mode", typeof(IBundleEntrySearchMode), Cardinality.Optional)]
[FhirProperty("score", typeof(IBundleEntrySearchScore), Cardinality.Optional)]
public interface IBundleEntrySearch : IElement;
