using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter businessStatus element (R6).</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>code</term>
///         <description><see cref="IEncounterBusinessStatusCode"/> (HL7 FHIR R6)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IEncounterBusinessStatusType"/> (HL7 FHIR R6)</description>
///     </item>
///     <item>
///         <term>effectiveDate</term>
///         <description><see cref="IEncounterBusinessStatusEffectiveDate"/> (HL7 FHIR R6)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("code", typeof(IEncounterBusinessStatusCode), Cardinality.Mandatory)]
[FhirProperty("type", typeof(IEncounterBusinessStatusType), Cardinality.Optional)]
[FhirProperty("effectiveDate", typeof(IEncounterBusinessStatusEffectiveDate), Cardinality.Optional)]
public interface IEncounterBusinessStatus : IElement;
