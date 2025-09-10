using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter participant element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>type</term>
///         <description><see cref="IEncounterParticipantType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IEncounterParticipantPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>individual</term>
///         <description><see cref="IEncounterParticipantIndividual"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>actor</term>
///         <description><see cref="IEncounterParticipantActor"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("type", typeof(IEncounterParticipantType), Cardinality.Multiple)]
[FhirProperty("period", typeof(IEncounterParticipantPeriod), Cardinality.Optional)]
[FhirProperty("individual", typeof(IEncounterParticipantIndividual), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("actor", typeof(IEncounterParticipantActor), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IEncounterParticipant : IElement;
