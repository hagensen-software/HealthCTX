using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterType;

public record Status(string Value) : IEncounterStatus;

public record TypeSystem(Uri Value) : ICodingSystem;
public record TypeCode(string Value) : ICodingCode;
public record TypeCoding(TypeSystem? System, TypeCode? Code) : ICodeableConceptCoding;

public record EncounterType(ImmutableList<TypeCoding> Codings) : IEncounterType;

public record Encounter(Status Status, ImmutableList<EncounterType> Types) : IEncounter;
