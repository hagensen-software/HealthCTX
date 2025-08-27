using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterPartOf;

public record Status(string Value) : IEncounterStatus;

public record EncounterReference(string Value) : IReferenceReference;
public record PartOfEncounter(EncounterReference? Reference) : IEncounterPartOf;

public record Encounter(Status Status, PartOfEncounter? PartOf) : IEncounter;
