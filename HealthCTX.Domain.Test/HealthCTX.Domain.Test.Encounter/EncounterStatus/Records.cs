using HealthCTX.Domain.Encounters;

namespace HealthCTX.Domain.Test.Encounter.EncounterStatus;

public record Status(string Value) : IEncounterStatus;

public record Encounter(Status Status) : IEncounter;
