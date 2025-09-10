using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter resource.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>identifier</term>
///         <description><see cref="IEncounterIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>status</term>
///         <description><see cref="IEncounterStatus"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>statusHistory</term>
///         <description><see cref="IEncounterStatusHistory"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>class</term>
///         <description><see cref="IEncounterClassCoding"/> (HL7 FHIR R4 Only) or <see cref="IEncounterClass"/> (HL7 FHIR R5)</description>
///     </item>
///     <item>
///         <term>classHistory</term>
///         <description><see cref="IEncounterClassHistory"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>priority</term>
///         <description><see cref="IEncounterPriority"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IEncounterType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>serviceType</term>
///         <description><see cref="IEncounterServiceTypeConcept"/> (HL7 FHIR R4 Only) or <see cref="IEncounterServiceType"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>subject</term>
///         <description><see cref="IEncounterSubject"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>subjectStatus</term>
///         <description><see cref="IEncounterSubjectStatus"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>episodeOfCare</term>
///         <description><see cref="IEncounterEpisodeOfCare"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>basedOn</term>
///         <description><see cref="IEncounterBasedOn"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>careTeam</term>
///         <description><see cref="IEncounterCareTeam"/> (HL7 FHIR R5)</description>
///     </item>
///     <item>
///         <term>partOf</term>
///         <description><see cref="IEncounterPartOf"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>serviceProvider</term>
///         <description><see cref="IEncounterServiceProvider"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>participant</term>
///         <description><see cref="IEncounterParticipant"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>appointment</term>
///         <description><see cref="IEncounterAppointment"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>virtualService</term>
///         <description><see cref="IEncounterVirtualService"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IEncounterPeriod"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>actualPeriod</term>
///         <description><see cref="IEncounterActualPeriod"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>plannedStartDate</term>
///         <description><see cref="IEncounterPlannedStartDate"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>plannedEndDate</term>
///         <description><see cref="IEncounterPlannedEndDate"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>length</term>
///         <description><see cref="IEncounterLength"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>reasonCode</term>
///         <description><see cref="IEncounterReasonCode"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>reasonReference</term>
///         <description><see cref="IEncounterReasonReference"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>reason</term>
///         <description><see cref="IEncounterReason"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>diagnosis</term>
///         <description><see cref="IEncounterDiagnosis"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>account</term>
///         <description><see cref="IEncounterAccount"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>dietPreference</term>
///         <description><see cref="IEncounterDietPreference"/> (HL7 FHIR R5)</description>
///     </item>
///     <item>
///         <term>specialArrangement</term>
///         <description><see cref="IEncounterSpecialArrangement"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>specialCourtesy</term>
///         <description><see cref="IEncounterSpecialCourtesy"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>hospitalization</term>
///         <description><see cref="IEncounterHospitalization"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>admission</term>
///         <description><see cref="IEncounterAdmission"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>location</term>
///         <description><see cref="IEncounterLocation"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirResource("Encounter")]
[FhirProperty("identifier", typeof(IEncounterIdentifier), Cardinality.Multiple)]
[FhirProperty("status", typeof(IEncounterStatus), Cardinality.Mandatory)]
[FhirProperty("statusHistory", typeof(IEncounterStatusHistory), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("class", typeof(IEncounterClassCoding), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("class", typeof(IEncounterClass), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("classHistory", typeof(IEncounterClassHistory), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("priority", typeof(IEncounterPriority), Cardinality.Optional)]
[FhirProperty("type", typeof(IEncounterType), Cardinality.Multiple)]
[FhirProperty("serviceType", typeof(IEncounterServiceTypeConcept), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("serviceType", typeof(IEncounterServiceType), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("subject", typeof(IEncounterSubject), Cardinality.Optional)]
[FhirProperty("subjectStatus", typeof(IEncounterSubjectStatus), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("episodeOfCare", typeof(IEncounterEpisodeOfCare), Cardinality.Multiple)]
[FhirProperty("basedOn", typeof(IEncounterBasedOn), Cardinality.Multiple)]
[FhirProperty("careTeam", typeof(IEncounterCareTeam), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("partOf", typeof(IEncounterPartOf), Cardinality.Optional)]
[FhirProperty("serviceProvider", typeof(IEncounterServiceProvider), Cardinality.Optional)]
[FhirProperty("participant", typeof(IEncounterParticipant), Cardinality.Multiple)]
[FhirProperty("appointment", typeof(IEncounterAppointment), Cardinality.Multiple)]
[FhirProperty("virtualService", typeof(IEncounterVirtualService), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("period", typeof(IEncounterPeriod), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("actualPeriod", typeof(IEncounterActualPeriod), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("plannedStartDate", typeof(IEncounterPlannedStartDate), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("plannedEndDate", typeof(IEncounterPlannedEndDate), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("length", typeof(IEncounterLength), Cardinality.Optional)]
[FhirProperty("reasonCode", typeof(IEncounterReasonCode), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("reasonReference", typeof(IEncounterReasonReference), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("reason", typeof(IEncounterReason), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("diagnosis", typeof(IEncounterDiagnosis), Cardinality.Multiple)]
[FhirProperty("account", typeof(IEncounterAccount), Cardinality.Multiple)]
[FhirProperty("dietPreference", typeof(IEncounterDietPreference), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("specialArrangement", typeof(IEncounterSpecialArrangement), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("specialCourtesy", typeof(IEncounterSpecialCourtesy), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("hospitalization", typeof(IEncounterHospitalization), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("admission", typeof(IEncounterAdmission), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("location", typeof(IEncounterLocation), Cardinality.Multiple)]
public interface IEncounter : IResource;
