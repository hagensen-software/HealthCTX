using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.Quantity;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationReferenceRange;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record LowValue(double Value) : IQuantityValue;
public record ReferenceRangeLow(LowValue Value) : IObservationReferenceRangeLow;
public record HighValue(double Value) : IQuantityValue;
public record ReferenceRangeHigh(HighValue Value) : IObservationReferenceRangeHigh;
public record ReferenceRange(ReferenceRangeLow Low, ReferenceRangeHigh High) : IObservationReferenceRange;

public record Observation(Status Status, ObservationCode Code, ImmutableList<ReferenceRange> ReferenceRanges) : IObservation;
