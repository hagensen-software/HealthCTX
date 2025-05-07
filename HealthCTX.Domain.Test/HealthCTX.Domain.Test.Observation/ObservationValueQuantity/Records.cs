using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.Quantity;

namespace HealthCTX.Domain.Test.Observation.ObservationValueQuantity;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ValueQuantityValue(double Value) : IQuantityValue;
public record ValueQuantity(ValueQuantityValue Quantity) : IObservationValueQuantity;

public record Observation(Status Status, ObservationCode Code, ValueQuantity Value) : IObservation;
