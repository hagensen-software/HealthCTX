using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterPriority;

public record Status(string Value) : IEncounterStatus;

public record PrioritySystem(Uri Value) : ICodingSystem;
public record PriorityCode(string Value) : ICodingCode;
public record PriorityCoding(PrioritySystem? System, PriorityCode? Code) : ICodeableConceptCoding;

public record EncounterPriority(ImmutableList<PriorityCoding> Codings) : IEncounterPriority;

public record Encounter(Status Status, EncounterPriority? Priority) : IEncounter;
