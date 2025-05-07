using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentDataAbsentReason;

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


public record ReasonCode(string Value) : ICodingCode;
public record ReasonSystem(Uri Value) : ICodingSystem;
public record ReasonCoding(
    ReasonCode Code,
    ReasonSystem System) : ICodeableConceptCoding;

public record DataAbsentReason(ReasonCoding Coding) : IObservationComponentDataAbsentReason;

public record ObservationComponent(ObservationComponentCode Code, DataAbsentReason DataAbsentReason) : IObservationComponent;

public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationComponent> Components) : IObservation;
