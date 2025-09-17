using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter statusHistory.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>status</term>
///         <description><see cref="IEncounterStatusHistoryStatus"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IEncounterStatusHistoryPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("status", typeof(IEncounterStatusHistoryStatus), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IEncounterStatusHistoryPeriod), Cardinality.Mandatory)]
public interface IEncounterStatusHistory : IElement;
