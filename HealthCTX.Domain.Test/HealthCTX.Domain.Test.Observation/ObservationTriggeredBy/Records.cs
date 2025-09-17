using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationTriggeredBy;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ObservationReference(string Value) : IReferenceReference;
public record TriggeredByObservation(ObservationReference Reference) : IObservationTriggeredByObservation;
public record TriggeredByType(string Value) : IObservationTriggeredByType;
public record TriggeredByReason(string Value) : IObservationTriggeredByReason;
public record TriggeredBy(
    TriggeredByObservation Observation,
    TriggeredByType Type,
    TriggeredByReason? Reason) : IObservationTriggeredBy;

public record Observation(Status Status, ObservationCode Code, ImmutableList<TriggeredBy> TriggeredBy) : IObservation;