using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterClassR5;

public record Status(string Value) : IEncounterStatus;

public record ClassSystem(Uri Value) : ICodingSystem;
public record ClassCode(string Value) : ICodingCode;
public record ClassCoding(ClassSystem? System, ClassCode? Code) : ICodeableConceptCoding;
public record EncounterClass(ImmutableList<ClassCoding> Codings) : IEncounterClass;

public record Encounter(Status Status, ImmutableList<EncounterClass> Classes) : IEncounter;
