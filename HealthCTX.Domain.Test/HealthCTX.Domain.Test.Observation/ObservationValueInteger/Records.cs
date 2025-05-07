using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;

namespace HealthCTX.Domain.Test.Observation.ObservationValueInteger;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ValueInteger(int Value) : IObservationValueInteger;

public record Observation(Status Status, ObservationCode Code, ValueInteger Value) : IObservation;
