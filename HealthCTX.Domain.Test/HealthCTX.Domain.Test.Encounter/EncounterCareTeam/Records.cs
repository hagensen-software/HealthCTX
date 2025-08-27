using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterCareTeam;

public record Status(string Value) : IEncounterStatus;

public record CareTeamReference(string Value) : IReferenceReference;
public record CareTeam(CareTeamReference? Reference) : IEncounterCareTeam;

public record Encounter(Status Status, ImmutableList<CareTeam> CareTeams) : IEncounter;
