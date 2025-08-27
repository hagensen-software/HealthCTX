using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterAppointment;

public record Status(string Value) : IEncounterStatus;

public record AppointmentReference(string Value) : IReferenceReference;
public record EncounterAppointment(AppointmentReference Reference) : IEncounterAppointment;

public record Encounter(Status Status, ImmutableList<EncounterAppointment> Appointments) : IEncounter;
