using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR ELEMENT ATTRIBUTE element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>condition</term>
///         <description><see cref="IEncounterDiagnosisConditionReference"/> (HL7 FHIR R4 only) or <see cref="IEncounterDiagnosisCondition"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>use</term>
///         <description><see cref="IEncounterDiagnosisUse"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>rank</term>
///         <description><see cref="IEncounterDiagnosisRank"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("condition", typeof(IEncounterDiagnosisConditionReference), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("condition", typeof(IEncounterDiagnosisCondition), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("use", typeof(IEncounterDiagnosisUse), Cardinality.Optional)]
[FhirProperty("rank", typeof(IEncounterDiagnosisRank), Cardinality.Optional, ToVersion: FhirVersion.R4)]
public interface IEncounterDiagnosis : IElement;
