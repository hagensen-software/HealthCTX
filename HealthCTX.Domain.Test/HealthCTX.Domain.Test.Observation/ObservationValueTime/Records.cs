using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;

namespace HealthCTX.Domain.Test.Observation.ObservationValueTime;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ValueTime(TimeOnly Value) : IObservationValueTime;

public record Observation(Status Status, ObservationCode Code, ValueTime Value) : IObservation;
