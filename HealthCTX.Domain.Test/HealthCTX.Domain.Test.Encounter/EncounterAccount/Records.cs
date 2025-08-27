using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterAccount;

public record Status(string Value) : IEncounterStatus;

public record AccountReference(string Value) : IReferenceReference;
public record EncounterAccount(AccountReference Reference) : IEncounterAccount;

public record Encounter(Status Status, ImmutableList<EncounterAccount> Appointments) : IEncounter;
