using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.SubjectStatus;

public record Status(string Value) : IEncounterStatus;

public record SubjectSStatusSystem(Uri Value) : ICodingSystem;
public record SubjectSStatusCode(string Value) : ICodingCode;

public record SubjectSStatusCoding(SubjectSStatusSystem System, SubjectSStatusCode Code) : ICodeableConceptCoding;

public record SubjectStatus(ImmutableList<SubjectSStatusCoding> Codings) : IEncounterSubjectStatus;

public record Encounter(Status Status, SubjectStatus? SubjectStatus) : IEncounter;
