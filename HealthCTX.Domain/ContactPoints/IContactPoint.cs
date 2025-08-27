using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.ContactPoints;

/// <summary>
/// <para>Interface for the HL7 FHIR ContactPoint element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>system</term>
///         <description><see cref="IContactPointSystem"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value</term>
///         <description><see cref="IContactPointValue"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>use</term>
///         <description><see cref="IContactPointUse"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>rank</term>
///         <description><see cref="IContactPointRank"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IContactPointPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("system", typeof(IContactPointSystem), Cardinality.Optional)]
[FhirProperty("value", typeof(IContactPointValue), Cardinality.Optional)]
[FhirProperty("use", typeof(IContactPointUse), Cardinality.Optional)]
[FhirProperty("rank", typeof(IContactPointRank), Cardinality.Optional)]
[FhirProperty("period", typeof(IContactPointPeriod), Cardinality.Optional)]
public interface IContactPoint : IElement;
