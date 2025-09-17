using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValueInteger;

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

public record ValueInteger(int Value) : IObservationComponentValueInteger;

public record ObservationComponent(ObservationComponentCode Code, ValueInteger Value) : IObservationComponent;


public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationComponent> Components) : IObservation;
