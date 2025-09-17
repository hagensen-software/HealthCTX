using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Periods;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterClassHistory;

public record Status(string Value) : IEncounterStatus;

public record ClassSystem(Uri Value) : ICodingSystem;
public record ClassCode(string Value) : ICodingCode;
public record ClassCoding(ClassSystem? System, ClassCode? Code) : IEncounterClassHistoryClass;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record HistoryPeriod(PeriodStart? Start, PeriodEnd? End) : IEncounterClassHistoryPeriod;

public record ClassHistory(ClassCoding Class, HistoryPeriod Period) : IEncounterClassHistory;

public record Encounter(Status Status, ImmutableList<ClassHistory> StatusHistories) : IEncounter;
