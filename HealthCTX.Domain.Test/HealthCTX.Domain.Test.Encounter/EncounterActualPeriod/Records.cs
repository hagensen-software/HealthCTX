using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Period;

namespace HealthCTX.Domain.Test.Encounter.EncounterActualPeriod;

public record Status(string Value) : IEncounterStatus;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record EncounterActualPeriod(PeriodStart Start, PeriodEnd End) : IEncounterActualPeriod;

public record Encounter(Status Status, EncounterActualPeriod ActualPeriod) : IEncounter;
