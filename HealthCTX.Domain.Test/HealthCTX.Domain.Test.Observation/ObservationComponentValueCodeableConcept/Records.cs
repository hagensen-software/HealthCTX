using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValueCodeableConcept;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ComponentCode(string Value) : ICodingCode;
public record ComponentCodeSystem(Uri Value) : ICodingSystem;

public record ObservationComponentCoding(
    ComponentCode Code,
    ComponentCodeSystem System) : ICodeableConceptCoding;

public record ObservationComponentCode(ObservationComponentCoding Coding) : IObservationComponentCode;

public record ValueCode(string Value) : ICodingCode;
public record ValueCoding(ValueCode Code) : ICodeableConceptCoding;
public record ValueCodeableConcept(ValueCoding Coding) : IObservationComponentValueCodeableConcept;

public record ObservationComponent(ObservationComponentCode Code, ValueCodeableConcept Value) : IObservationComponent;


public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationComponent> Components) : IObservation;

