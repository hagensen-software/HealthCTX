using HealthCTX.Domain.Encounters;

namespace HealthCTX.Domain.Test.Encounter.EncounterPlannedStartDate;

public record Status(string Value) : IEncounterStatus;

public record PlannedStartDate(DateTimeOffset Value) : IEncounterPlannedStartDate;

public record Encounter(Status Status, PlannedStartDate? PlannedStartDate) : IEncounter;
