using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.Quantity;
using HealthCTX.Domain.Ratio;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValueRatio;

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

public record Numerator(double Value) : IQuantityValue;
public record RatioNumerator(Numerator Value) : IRatioNumerator;
public record Denominator(double Value) : IQuantityValue;
public record RatioDenominator(Denominator Value) : IRatioDenominator;
public record ValueRatio(RatioNumerator Numerator, RatioDenominator Denominator) : IObservationComponentValueRatio;

public record ObservationComponent(ObservationComponentCode Code, ValueRatio Value) : IObservationComponent;

public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationComponent> Components) : IObservation;
