using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Periods;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterStatusHistory;

public record Status(string Value) : IEncounterStatus;

public record HistoryStatus(string Value) : IEncounterStatusHistoryStatus;
public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;

public record HistoryPeriod(PeriodStart? Start, PeriodEnd? End) : IEncounterStatusHistoryPeriod;
public record StatusHistory(HistoryStatus Status, HistoryPeriod Period) : IEncounterStatusHistory;

public record Encounter(Status Status, ImmutableList<StatusHistory> StatusHistories) : IEncounter;
