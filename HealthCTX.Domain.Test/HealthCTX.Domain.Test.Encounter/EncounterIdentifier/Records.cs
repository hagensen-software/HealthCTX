using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Identifiers;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterIdentifier;

public record IdentifierSystem(Uri Value) : IIdentifierSystem;
public record IdentifierValue(string Value) : IIdentifierValue;
public record Identifier(IdentifierSystem System, IdentifierValue Value) : IEncounterIdentifier;

public record Status(string Value) : IEncounterStatus;

public record Encounter(ImmutableList<Identifier> Identifiers, Status Status) : IEncounter;