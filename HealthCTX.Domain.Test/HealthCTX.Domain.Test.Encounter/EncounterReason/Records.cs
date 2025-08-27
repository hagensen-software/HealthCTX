using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.CodeableReferences;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterReason;

public record Status(string Value) : IEncounterStatus;

public record UseSystem(Uri Value) : ICodingSystem;
public record UseCode(string Value) : ICodingCode;
public record UseCoding(UseSystem? System, UseCode? Code) : ICodeableConceptCoding;
public record ReasonUse(ImmutableList<UseCoding> Codings) : IEncounterReasonUse;


public record ReasonReferenceReference(string Value) : IReferenceReference;
public record ReasonReference(ReasonReferenceReference Value) : ICodeableReferenceReference;
public record ReasonValue(ReasonReference Reference) : IEncounterReasonValue;

public record EncounterReason(ImmutableList<ReasonUse> Uses, ImmutableList<ReasonValue> Values) : IEncounterReason;

public record Encounter(Status Status, ImmutableList<EncounterReason> Reasons) : IEncounter;
