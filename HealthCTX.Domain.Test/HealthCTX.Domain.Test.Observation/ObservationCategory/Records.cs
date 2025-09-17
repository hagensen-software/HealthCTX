using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationCategory;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ObservationCategoryCode(string Value) : ICodingCode;
public record ObservationCategorySystem(Uri Value) : ICodingSystem;
public record ObservationCategoryCoding(
    ObservationCategoryCode Code,
    ObservationCategorySystem System) : ICodeableConceptCoding;

public record ObservationCategory(ObservationCategoryCoding Coding) : IObservationCategory;

public record Observation(Status Status, ObservationCode Code, ImmutableList<ObservationCategory> Categories) : IObservation;
