using HealthCTX.Domain.Annotation;
using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationNote;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;



public record Author(string Value) : IAnnotationAuthorString;
public record AuthoredTime(DateTimeOffset Value) : IAnnotationTime;
public record Text(string Value) : IAnnotationText;

public record ObservationNote(Author? Author, AuthoredTime? Time, Text Text) : IObservationNote;


public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationNote> Notes) : IObservation;
