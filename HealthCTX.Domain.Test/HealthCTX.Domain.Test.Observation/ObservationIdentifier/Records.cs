using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Observation;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationIdentifier;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ObservationIdentifierSystem(Uri Value) : IIdentifierSystem;
public record ObservationIdentifierValue(string Value) : IIdentifierValue;
public record ObservationIdentifier(ObservationIdentifierSystem System, ObservationIdentifierValue Value) : IObservationIdentifier;

public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationIdentifier> Identifiers) : IObservation;
