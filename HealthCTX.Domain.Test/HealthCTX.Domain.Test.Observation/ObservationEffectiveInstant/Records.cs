using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;

namespace HealthCTX.Domain.Test.Observation.ObservationEffectiveInstant;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ObservationEffectiveInstant(DateTimeOffset Value) : IObservationEffectiveInstant;

public record Observation(Status Status, ObservationCode Code, ObservationEffectiveInstant? Effective) : IObservation;
