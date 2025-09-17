using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.Periods;

namespace HealthCTX.Domain.Test.Observation.ObservationValuePeriod;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;

public record ValuePeriod(PeriodStart? Start, PeriodEnd? End) : IObservationValuePeriod;

public record Observation(Status Status, ObservationCode Code, ValuePeriod Value) : IObservation;
