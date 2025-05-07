using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Observation.ObservationValueReference;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ValueReference(string Value) : IReferenceReference;
public record Value(ValueReference Reference) : IObservationValueReference;

public record Observation(Status Status, ObservationCode Code, Value? Value) : IObservation;
