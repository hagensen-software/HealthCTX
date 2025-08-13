using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterHospitalization;

public record Status(string Value) : IEncounterStatus;

public record IdentifierSystem(Uri Value) : IIdentifierSystem;
public record IdentifierValue(string Value) : IIdentifierValue;
public record HospitalizationPreAdmissionIdentifier(IdentifierSystem System, IdentifierValue Value) : IEncounterAdmissionPreAdmissionIdentifier;

public record OriginReferenceReference(string Value) : IReferenceReference;
public record HospitalizationOrigin(OriginReferenceReference Reference) : IEncounterAdmissionOrigin;

public record AdmitSourceSystem(Uri Value) : ICodingSystem;
public record AdmitSourceCode(string Value) : ICodingCode;
public record AdmitSourceCoding(AdmitSourceSystem? System, AdmitSourceCode? Code) : ICodeableConceptCoding;
public record HospitalizationAdmitSource(ImmutableList<AdmitSourceCoding> Codings) : IEncounterAdmissionAdmitSource;

public record ReAdmissionSystem(Uri Value) : ICodingSystem;
public record ReAdmissionCode(string Value) : ICodingCode;
public record ReAdmissionCoding(ReAdmissionSystem? System, ReAdmissionCode? Code) : ICodeableConceptCoding;
public record HospitalizationReAdmission(ImmutableList<ReAdmissionCoding> Codings) : IEncounterAdmissionReAdmission;

public record DietPreferenceSystem(Uri Value) : ICodingSystem;
public record DietPreferenceCode(string Value) : ICodingCode;
public record DietPreferenceCoding(DietPreferenceSystem? System, DietPreferenceCode? Code) : ICodeableConceptCoding;
public record HospitalizationDietPreference(ImmutableList<DietPreferenceCoding> Codings) : IEncounterDietPreference;

public record SpecialCourtesySystem(Uri Value) : ICodingSystem;
public record SpecialCourtesyCode(string Value) : ICodingCode;
public record SpecialCourtesyCoding(SpecialCourtesySystem? System, SpecialCourtesyCode? Code) : ICodeableConceptCoding;
public record HospitalizationSpecialCourtesy(ImmutableList<SpecialCourtesyCoding> Codings) : IEncounterSpecialCourtesy;

public record SpecialArrangementSystem(Uri Value) : ICodingSystem;
public record SpecialArrangementCode(string Value) : ICodingCode;
public record SpecialArrangementCoding(SpecialArrangementSystem? System, SpecialArrangementCode? Code) : ICodeableConceptCoding;
public record HospitalizationSpecialArrangement(ImmutableList<SpecialArrangementCoding> Codings) : IEncounterSpecialArrangement;

public record DestinationReferenceReference(string Value) : IReferenceReference;
public record HospitalizationDestination(DestinationReferenceReference Reference) : IEncounterAdmissionDestination;

public record DischargeDispositionSystem(Uri Value) : ICodingSystem;
public record DischargeDispositionCode(string Value) : ICodingCode;
public record DischargeDispositionCoding(DischargeDispositionSystem? System, DischargeDispositionCode? Code) : ICodeableConceptCoding;
public record HospitalizationDischargeDisposition(ImmutableList<DischargeDispositionCoding> Codings) : IEncounterAdmissionDischargeDisposition;

public record EncounterHospitalization(
    HospitalizationPreAdmissionIdentifier? PreAdmissionIdentifier,
    HospitalizationOrigin? Origin,
    HospitalizationAdmitSource? AdmitSource,
    HospitalizationReAdmission? ReAdmission,
    ImmutableList<HospitalizationDietPreference> DietPreferences,
    ImmutableList<HospitalizationSpecialCourtesy> SpecialCourtesies,
    ImmutableList<HospitalizationSpecialArrangement> SpecialArrangements,
    HospitalizationDestination? Destination,
    HospitalizationDischargeDisposition? DischargeDisposition
    ) : IEncounterHospitalization;

public record Encounter(Status Status, EncounterHospitalization Hospitalization) : IEncounter;
