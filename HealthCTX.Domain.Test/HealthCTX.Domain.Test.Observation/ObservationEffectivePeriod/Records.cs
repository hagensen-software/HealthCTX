using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.Periods;

namespace HealthCTX.Domain.Test.Observation.ObservationEffectivePeriod;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;

public record ObservationEffectivePeriod(PeriodStart? Start, PeriodEnd? End) : IObservationEffectivePeriod;

public record Observation(Status Status, ObservationCode Code, ObservationEffectivePeriod Effective) : IObservation;
