using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterReasonReference;

public record Status(string Value) : IEncounterStatus;

public record ReasonReference(string Value) : IReferenceReference;
public record EncounterReasonReference(ReasonReference Reference) : IEncounterReasonReference;

public record Encounter(Status Status, ImmutableList<EncounterReasonReference> ReasonReferences) : IEncounter;
