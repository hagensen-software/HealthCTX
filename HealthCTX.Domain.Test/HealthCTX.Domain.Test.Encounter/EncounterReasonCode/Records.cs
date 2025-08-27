using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterReasonCode;

public record Status(string Value) : IEncounterStatus;

public record ReasonCodeSystem(Uri Value) : ICodingSystem;
public record ReasonCodeCode(string Value) : ICodingCode;
public record ReasonCodeCoding(ReasonCodeSystem? System, ReasonCodeCode? Code) : ICodeableConceptCoding;

public record EncounterReasonCode(ImmutableList<ReasonCodeCoding> Codings) : IEncounterReasonCode;

public record Encounter(Status Status, ImmutableList<EncounterReasonCode> ReasonCodes) : IEncounter;
