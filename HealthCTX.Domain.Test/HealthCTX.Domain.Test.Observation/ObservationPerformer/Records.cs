using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationPerformer;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ObservationPerformerReference(string Value) : IReferenceReference;
public record ObservationPerformer(ObservationPerformerReference Reference) : IObservationPerformer;

public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationPerformer> Performer) : IObservation;
