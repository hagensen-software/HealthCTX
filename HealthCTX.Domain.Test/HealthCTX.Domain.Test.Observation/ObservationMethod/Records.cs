using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;

namespace HealthCTX.Domain.Test.Observation.ObservationMethod;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record MethodCode(string Value) : ICodingCode;
public record MethodSystem(Uri Value) : ICodingSystem;
public record MethodCoding(
    MethodCode Code,
    MethodSystem System) : ICodeableConceptCoding;

public record ObservationMethod(MethodCoding Coding) : IObservationMethod;

public record Observation(Status Status, ObservationCode Code, ObservationMethod? Method) : IObservation;
