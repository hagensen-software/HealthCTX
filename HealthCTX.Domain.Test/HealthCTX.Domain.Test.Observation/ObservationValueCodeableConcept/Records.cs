using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;

namespace HealthCTX.Domain.Test.Observation.ObservationValueCodeableConcept;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ValueCode(string Value) : ICodingCode;
public record ValueCoding(ValueCode Code) : ICodeableConceptCoding;
public record ValueCodeableConcept(ValueCoding Coding) : IObservationValueCodeableConcept;

public record Observation(Status Status, ObservationCode Code, ValueCodeableConcept Value) : IObservation;
