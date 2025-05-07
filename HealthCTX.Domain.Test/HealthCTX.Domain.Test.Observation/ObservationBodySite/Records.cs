using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;

namespace HealthCTX.Domain.Test.Observation.ObservationBodySite;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record BodySiteCode(string Value) : ICodingCode;
public record BodySiteSystem(Uri Value) : ICodingSystem;
public record BodySiteCoding(
    BodySiteCode Code,
    BodySiteSystem System) : ICodeableConceptCoding;
public record ObservationBodySite(BodySiteCoding Coding) : IObservationBodySite;

public record Observation(Status Status, ObservationCode Code, ObservationBodySite? BodySite) : IObservation;
