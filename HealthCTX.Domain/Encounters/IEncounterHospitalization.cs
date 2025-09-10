using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter admission element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>preAdmissionIdentifier</term>
///         <description><see cref="IEncounterAdmissionPreAdmissionIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>origin</term>
///         <description><see cref="IEncounterAdmissionOrigin"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>admitSource</term>
///         <description><see cref="IEncounterAdmissionAdmitSource"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>dietPreference</term>
///         <description><see cref="IEncounterDietPreference"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>specialCourtesy</term>
///         <description><see cref="IEncounterSpecialCourtesy"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>specialArrangement</term>
///         <description><see cref="IEncounterSpecialArrangement"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>reAdmission</term>
///         <description><see cref="IEncounterAdmissionReAdmission"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>destination</term>
///         <description><see cref="IEncounterAdmissionDestination"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>dischargeDisposition</term>
///         <description><see cref="IEncounterAdmissionDischargeDisposition"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("preAdmissionIdentifier", typeof(IEncounterAdmissionPreAdmissionIdentifier), Cardinality.Optional)]
[FhirProperty("origin", typeof(IEncounterAdmissionOrigin), Cardinality.Optional)]
[FhirProperty("admitSource", typeof(IEncounterAdmissionAdmitSource), Cardinality.Optional)]
[FhirProperty("dietPreference", typeof(IEncounterDietPreference), Cardinality.Multiple)]
[FhirProperty("specialCourtesy", typeof(IEncounterSpecialCourtesy), Cardinality.Multiple)]
[FhirProperty("specialArrangement", typeof(IEncounterSpecialArrangement), Cardinality.Multiple)]
[FhirProperty("reAdmission", typeof(IEncounterAdmissionReAdmission), Cardinality.Optional)]
[FhirProperty("destination", typeof(IEncounterAdmissionDestination), Cardinality.Optional)]
[FhirProperty("dischargeDisposition", typeof(IEncounterAdmissionDischargeDisposition), Cardinality.Optional)]
public interface IEncounterHospitalization : IElement;
