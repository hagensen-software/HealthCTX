using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterClassR4;

public record Status(string Value) : IEncounterStatus;

public record ClassSystem(Uri Value) : ICodingSystem;
public record ClassCode(string Value) : ICodingCode;
public record EncounterClass(ClassSystem? System, ClassCode? Code) : IEncounterClassCoding;

public record Encounter(Status Status, ImmutableList<EncounterClass> Classes) : IEncounter;
