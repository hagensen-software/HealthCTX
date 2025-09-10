using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundle;

/// <summary>
/// <para>Interface for the HL7 FHIR Bundle link and entry.link elements.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>relation</term>
///         <description><see cref="IBundleLinkRelation"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>url</term>
///         <description><see cref="IBundleLinkUrl"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("relation", typeof(IBundleLinkRelation), Cardinality.Mandatory)]
[FhirProperty("url", typeof(IBundleLinkUrl), Cardinality.Mandatory)]
public interface IBundleLink : IElement;
