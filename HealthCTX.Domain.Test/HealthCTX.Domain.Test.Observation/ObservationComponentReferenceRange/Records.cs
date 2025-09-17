using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.Quantities;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentReferenceRange;

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

public record LowValue(double Value) : IQuantityValue;
public record ReferenceRangeLow(LowValue Value) : IObservationReferenceRangeLow;
public record HighValue(double Value) : IQuantityValue;
public record ReferenceRangeHigh(HighValue Value) : IObservationReferenceRangeHigh;
public record ReferenceRange(ReferenceRangeLow Low, ReferenceRangeHigh High) : IObservationComponentReferenceRange;

public record ObservationComponent(ObservationComponentCode Code, ImmutableList<ReferenceRange> ReferenceRange) : IObservationComponent;


public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationComponent> Components) : IObservation;
