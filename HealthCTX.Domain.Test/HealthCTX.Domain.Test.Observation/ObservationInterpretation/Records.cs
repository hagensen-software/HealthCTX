using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationInterpretation;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record InterpretationCode(string Value) : ICodingCode;
public record InterpretationSystem(Uri Value) : ICodingSystem;
public record InterpretationCoding(
    InterpretationCode Code,
    InterpretationSystem System) : ICodeableConceptCoding;

public record ObservationInterpretation(InterpretationCoding Coding) : IObservationInterpretation;

public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationInterpretation> Interpretation) : IObservation;
