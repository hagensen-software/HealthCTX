using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;

namespace HealthCTX.Domain.Test.Observation.ObservationValueString;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ValueString(string Value) : IObservationValueString;

public record Observation(Status Status, ObservationCode Code, ValueString Value) : IObservation;
