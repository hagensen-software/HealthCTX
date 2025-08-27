using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Encounter.EncounterServiceProvider;

public record Status(string Value) : IEncounterStatus;

public record OrganizationReference(string Value) : IReferenceReference;
public record Organization(OrganizationReference? Reference) : IEncounterServiceProvider;

public record Encounter(Status Status, Organization? ServiceProvider) : IEncounter;
