using HealthCTX.Domain.Encounters;

namespace HealthCTX.Domain.Test.Encounter.EncounterPlannedEndDate;

public record Status(string Value) : IEncounterStatus;

public record PlannedEndDate(DateTimeOffset Value) : IEncounterPlannedEndDate;

public record Encounter(Status Status, PlannedEndDate? PlannedEndDate) : IEncounter;
