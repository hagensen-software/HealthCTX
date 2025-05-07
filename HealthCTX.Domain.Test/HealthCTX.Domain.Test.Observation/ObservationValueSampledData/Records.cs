using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.Quantity;
using HealthCTX.Domain.SampledData;

namespace HealthCTX.Domain.Test.Observation.ObservationValueSampledData;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;


public record OriginValue(double Value) : IQuantityValue;
public record OriginQuantity(OriginValue Value) : ISampledDataOrigin;
public record Period(double Value) : ISampledDataPeriod;
public record Interval(double Value) : ISampledDataInterval;
public record IntervalUnit(string Value) : ISampledDataIntervalUnit;
public record Factor(double Value) : ISampledDataFactor;
public record LowerLimit(double Value) : ISampledDataLowerLimit;
public record UpperLimit(double Value) : ISampledDataUpperLimit;
public record Dimensions(uint Value) : ISampledDataDimensions;
public record CodeMap(Uri Value) : ISampledDataCodeMap;
public record Offsets(string Value) : ISampledDataOffsets;
public record Data(string Value) : ISampledDataData;


public record SampledDataR4(
    OriginQuantity Origin,
    Period Period,
    Factor Factor,
    LowerLimit LowerLimit,
    UpperLimit UpperLimit,
    Dimensions Dimensions,
    Data Data) : IObservationValueSampledData;
public record SampledDataR5(
    OriginQuantity Origin,
    Interval Interval,
    IntervalUnit IntervalUnit,
    Factor Factor,
    LowerLimit LowerLimit,
    UpperLimit UpperLimit,
    Dimensions Dimensions,
    CodeMap CodeMap,
    Offsets Offsets,
    Data Data) : IObservationValueSampledData;

public record ObservationR4(Status Status, ObservationCode Code, SampledDataR4? SampledData) : IObservation;
public record ObservationR5(Status Status, ObservationCode Code, SampledDataR5? SampledData) : IObservation;
