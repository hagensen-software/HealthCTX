using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationOrganizerR6;

public record Status(string Value) : IObservationStatus;

public record CodeValue(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(CodeValue Code, CodeSystem System) : ICodeableConceptCoding;
public record ObservationCode(ImmutableList<ObservationCodeCoding> Codings) : IObservationCode;

public record ObservationOrganizer(bool Value) : IObservationOrganizer;

public record Observation(
    Status Status,
    ObservationCode Code,
    ObservationOrganizer? Organizer) : IObservation;
