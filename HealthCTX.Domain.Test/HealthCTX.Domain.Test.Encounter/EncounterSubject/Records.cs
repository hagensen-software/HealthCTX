using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Encounter.EncounterSubject;

public record Status(string Value) : IEncounterStatus;

public record SubjectReference(string Value) : IReferenceReference;
public record Subject(SubjectReference Reference) : IEncounterSubject;

public record Encounter(Status Status, Subject? Subject) : IEncounter;
