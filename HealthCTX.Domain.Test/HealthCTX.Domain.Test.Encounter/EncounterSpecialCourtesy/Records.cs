using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterSpecialCourtesy;

public record Status(string Value) : IEncounterStatus;

public record SpecialCourtesySystem(Uri Value) : ICodingSystem;
public record SpecialCourtesyCode(string Value) : ICodingCode;
public record SpecialCourtesyCoding(SpecialCourtesySystem? System, SpecialCourtesyCode? Code) : ICodeableConceptCoding;

public record EncounterSpecialCourtesy(ImmutableList<SpecialCourtesyCoding> Codings) : IEncounterSpecialCourtesy;

public record Encounter(Status Status, ImmutableList<EncounterSpecialCourtesy> SpecialCourtesies) : IEncounter;
