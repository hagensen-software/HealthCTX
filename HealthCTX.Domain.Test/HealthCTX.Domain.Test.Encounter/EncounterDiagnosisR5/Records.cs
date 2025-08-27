using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.CodeableReferences;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterDiagnosisR5;

public record Status(string Value) : IEncounterStatus;

public record ReferenceReference(string Value) : IReferenceReference;
public record ConditionReference(ReferenceReference Reference) : ICodeableReferenceReference;
public record DiagnosisReference(ConditionReference Reference) : IEncounterDiagnosisCondition;

public record DiagnosisUseSystem(Uri Value) : ICodingSystem;
public record DiagnosisUseCode(string Value) : ICodingCode;
public record DiagnosisUseCoding(DiagnosisUseSystem? System, DiagnosisUseCode? Code) : ICodeableConceptCoding;
public record DiagnosisUse(ImmutableList<DiagnosisUseCoding> Codings) : IEncounterDiagnosisUse;

public record EncounterDiagnosis(ImmutableList<DiagnosisReference> Conditions, DiagnosisUse? Use) : IEncounterDiagnosis;

public record Encounter(Status Status, ImmutableList<EncounterDiagnosis> Diagnosis) : IEncounter;
