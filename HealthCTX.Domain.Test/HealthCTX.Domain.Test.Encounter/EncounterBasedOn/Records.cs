using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterBasedOn;

public record Status(string Value) : IEncounterStatus;

public record BasedOnReference(string Value) : IReferenceReference;
public record BasedOn(BasedOnReference? Reference) : IEncounterBasedOn;

public record Encounter(Status Status, ImmutableList<BasedOn> BasedOn) : IEncounter;
