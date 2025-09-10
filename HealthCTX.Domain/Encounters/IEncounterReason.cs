using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter reason element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>use</term>
///         <description><see cref="IEncounterReasonUse"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value</term>
///         <description><see cref="IEncounterReasonValue"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("use", typeof(IEncounterReasonUse), Cardinality.Multiple)]
[FhirProperty("value", typeof(IEncounterReasonValue), Cardinality.Multiple)]
public interface IEncounterReason : IElement;
