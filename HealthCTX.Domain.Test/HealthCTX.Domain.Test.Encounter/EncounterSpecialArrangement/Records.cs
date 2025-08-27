using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterSpecialArrangement;

public record Status(string Value) : IEncounterStatus;

public record SpecialArrangementSystem(Uri Value) : ICodingSystem;
public record SpecialArrangementCode(string Value) : ICodingCode;
public record SpecialArrangementCoding(SpecialArrangementSystem? System, SpecialArrangementCode? Code) : ICodeableConceptCoding;

public record EncounterSpecialArrangement(ImmutableList<SpecialArrangementCoding> Codings) : IEncounterSpecialArrangement;

public record Encounter(Status Status, ImmutableList<EncounterSpecialArrangement> SpecialArrangements) : IEncounter;
