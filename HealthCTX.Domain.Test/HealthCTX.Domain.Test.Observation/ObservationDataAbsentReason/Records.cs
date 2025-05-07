using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;

namespace HealthCTX.Domain.Test.Observation.ObservationDataAbsentReason;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record ReasonCode(string Value) : ICodingCode;
public record ReasonSystem(Uri Value) : ICodingSystem;
public record ReasonCoding(
    ReasonCode Code,
    ReasonSystem System) : ICodeableConceptCoding;

public record DataAbsentReason(ReasonCoding Coding) : IObservationDataAbsentReason;

public record Observation(Status Status, ObservationCode Code, DataAbsentReason? DataAbsentReason) : IObservation;
