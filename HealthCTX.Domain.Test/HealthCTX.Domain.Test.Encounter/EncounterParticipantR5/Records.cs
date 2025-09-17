using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Periods;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterParticipantR5;

public record Status(string Value) : IEncounterStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ParticipantTypeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;
public record ParticipantType(ImmutableList<ParticipantTypeCoding> Codings) : IEncounterParticipantType;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record ParticipantPeriod(PeriodStart Start, PeriodEnd End) : IEncounterParticipantPeriod;

public record ActorReference(string Value) : IReferenceReference;
public record ParticipantActor(ActorReference Reference) : IEncounterParticipantActor;
public record EncounterParticipant(ImmutableList<ParticipantType> Types, ParticipantPeriod Period, ParticipantActor Actor) : IEncounterParticipant;

public record Encounter(Status Status, ImmutableList<EncounterParticipant> Participants) : IEncounter;
