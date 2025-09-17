using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.Quantities;
using HealthCTX.Domain.Ratios;

namespace HealthCTX.Domain.Test.Observation.ObservationValueRatio;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record Numerator(double Value) : IQuantityValue;
public record RatioNumerator(Numerator Value) : IRatioNumerator;
public record Denominator(double Value) : IQuantityValue;
public record RatioDenominator(Denominator Value) : IRatioDenominator;
public record ValueRatio(RatioNumerator Numerator, RatioDenominator Denominator) : IObservationValueRatio;

public record Observation(Status Status, ObservationCode Code, ValueRatio Ratio) : IObservation;
