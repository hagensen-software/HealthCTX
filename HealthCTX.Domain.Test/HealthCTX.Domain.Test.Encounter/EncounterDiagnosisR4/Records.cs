using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterDiagnosisR4;

public record Status(string Value) : IEncounterStatus;

public record ConditionReferenceReference(string Value) : IReferenceReference;
public record ConditionReference(ConditionReferenceReference Reference) : IEncounterDiagnosisConditionReference;

public record DiagnosisUseSystem(Uri Value) : ICodingSystem;
public record DiagnosisUseCode(string Value) : ICodingCode;
public record DiagnosisUseCoding(DiagnosisUseSystem? System, DiagnosisUseCode? Code) : ICodeableConceptCoding;
public record DiagnosisUse(ImmutableList<DiagnosisUseCoding> Codings) : IEncounterDiagnosisUse;

public record DiagnosisRank(uint Value) : IEncounterDiagnosisRank;

public record EncounterDiagnosis(ImmutableList<ConditionReference> Conditions, DiagnosisUse? Use, DiagnosisRank? Rank) : IEncounterDiagnosis;

public record Encounter(Status Status, ImmutableList<EncounterDiagnosis> Diagnosis) : IEncounter;
