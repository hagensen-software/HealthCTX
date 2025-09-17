using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.Quantities;
using HealthCTX.Domain.SampledData;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValueSampledData;

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
    Data Data) : IObservationComponentValueSampledData;
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
    Data Data) : IObservationComponentValueSampledData;

public record ObservationComponentR4(ObservationComponentCode Code, SampledDataR4 Value) : IObservationComponent;
public record ObservationR4(Status Status, ObservationCode Code, ImmutableList<ObservationComponentR4> Components) : IObservation;

public record ObservationComponentR5(ObservationComponentCode Code, SampledDataR5 Value) : IObservationComponent;
public record ObservationR5(Status Status, ObservationCode Code, ImmutableList<ObservationComponentR5> Components) : IObservation;
