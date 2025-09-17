using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationFocus;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record FocusReference(string Value) : IReferenceReference;
public record Focus(FocusReference Reference) : IObservationFocus;

public record Observation(Status Status, ObservationCode Code, ImmutableList<Focus> Focuses) : IObservation;
