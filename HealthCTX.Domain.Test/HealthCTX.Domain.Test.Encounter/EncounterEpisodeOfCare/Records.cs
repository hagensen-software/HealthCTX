using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterEpisodeOfCare;

public record Status(string Value) : IEncounterStatus;

public record EpisodeOfCareReference(string Value) : IReferenceReference;
public record EpisodeOfCare(EpisodeOfCareReference? Reference) : IEncounterEpisodeOfCare;

public record Encounter(Status Status, ImmutableList<EpisodeOfCare> EpisodeOfCares) : IEncounter;
