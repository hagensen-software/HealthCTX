using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Observation.ObservationEncounter;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record EncounterReference(string Value) : IReferenceReference;
public record Encounter(EncounterReference Reference) : IObservationEncounter;

public record Observation(Status Status, ObservationCode Code, Encounter? Subject) : IObservation;
