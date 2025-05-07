using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.Quantity;
using HealthCTX.Domain.Range;

namespace HealthCTX.Domain.Test.Observation.ObservationValueRange;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record LowValue(double Value) : IQuantityValue;
public record ObservationRangeLow(LowValue Value) : IRangeLow;
public record HighValue(double Value) : IQuantityValue;
public record ObservationRangeHigh(HighValue Value) : IRangeHigh;
public record ValueRange(ObservationRangeLow Low, ObservationRangeHigh High) : IObservationValueRange;

public record Observation(Status Status, ObservationCode Code, ValueRange Value) : IObservation;
