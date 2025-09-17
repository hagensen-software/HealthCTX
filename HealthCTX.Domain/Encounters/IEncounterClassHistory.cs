using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter classHistory.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>class</term>
///         <description><see cref="IEncounterClassHistoryClass"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IEncounterClassHistoryPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("class", typeof(IEncounterClassHistoryClass), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IEncounterClassHistoryPeriod), Cardinality.Mandatory)]
public interface IEncounterClassHistory : IElement;
