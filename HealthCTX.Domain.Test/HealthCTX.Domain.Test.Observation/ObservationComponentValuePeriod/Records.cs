using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.Period;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationComponentValuePeriod;

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

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;

public record ValuePeriod(PeriodStart? Start, PeriodEnd? End) : IObservationComponentValuePeriod;

public record ObservationComponent(ObservationComponentCode Code, ValuePeriod Value) : IObservationComponent;


public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationComponent> Components) : IObservation;
