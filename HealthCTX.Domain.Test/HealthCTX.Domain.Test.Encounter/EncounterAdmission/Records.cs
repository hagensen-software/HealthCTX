using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterAdmission;

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

public record DestinationReferenceReference(string Value) : IReferenceReference;
public record HospitalizationDestination(DestinationReferenceReference Reference) : IEncounterAdmissionDestination;

public record DischargeDispositionSystem(Uri Value) : ICodingSystem;
public record DischargeDispositionCode(string Value) : ICodingCode;
public record DischargeDispositionCoding(DischargeDispositionSystem? System, DischargeDispositionCode? Code) : ICodeableConceptCoding;
public record HospitalizationDischargeDisposition(ImmutableList<DischargeDispositionCoding> Codings) : IEncounterAdmissionDischargeDisposition;

public record EncounterAdmission(
    HospitalizationPreAdmissionIdentifier? PreAdmissionIdentifier,
    HospitalizationOrigin? Origin,
    HospitalizationAdmitSource? AdmitSource,
    HospitalizationReAdmission? ReAdmission,
    HospitalizationDestination? Destination,
    HospitalizationDischargeDisposition? DischargeDisposition
    ) : IEncounterAdmission;

public record Encounter(Status Status, EncounterAdmission Admission) : IEncounter;
