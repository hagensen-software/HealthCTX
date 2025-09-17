using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.Timings;

namespace HealthCTX.Domain.Test.Observation.ObservationEffectiveTiming;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;


public record TimingEvent(DateTimeOffset Value) : ITimingEvent;
public record EffectiveTiming(TimingEvent? Event) : IObservationEffectiveTiming;

public record Observation(Status Status, ObservationCode Code, EffectiveTiming? Effective) : IObservation;
