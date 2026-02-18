using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterBusinessStatusR6;

public record Status(string Value) : IEncounterStatus;

public record BusinessStatusCodeCodingCode(string Value) : ICodingCode;
public record BusinessStatusCodeCoding(BusinessStatusCodeCodingCode Code) : ICodeableConceptCoding;
public record BusinessStatusCode(ImmutableList<BusinessStatusCodeCoding> Codings) : IEncounterBusinessStatusCode;

public record BusinessStatusTypeCodingCode(string Value) : ICodingCode;
public record BusinessStatusTypeCoding(BusinessStatusTypeCodingCode Code) : ICodeableConceptCoding;
public record BusinessStatusType(ImmutableList<BusinessStatusTypeCoding> Codings) : IEncounterBusinessStatusType;

public record BusinessStatusEffectiveDate(DateTimeOffset Value) : IEncounterBusinessStatusEffectiveDate;

public record EncounterBusinessStatus(
    BusinessStatusCode Code,
    BusinessStatusType? Type,
    BusinessStatusEffectiveDate? EffectiveDate) : IEncounterBusinessStatus;

public record Encounter(
    Status Status,
    ImmutableList<EncounterBusinessStatus> BusinessStatuses) : IEncounter;
