using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Period;

namespace HealthCTX.Domain.Test.Encounter.EncounterPeriod;

public record Status(string Value) : IEncounterStatus;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record EncounterPeriod(PeriodStart Start, PeriodEnd End) : IEncounterPeriod;

public record Encounter(Status Status, EncounterPeriod Period) : IEncounter;
