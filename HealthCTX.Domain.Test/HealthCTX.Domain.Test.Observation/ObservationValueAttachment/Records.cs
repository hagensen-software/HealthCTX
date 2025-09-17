using HealthCTX.Domain.Attachments;
using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;

namespace HealthCTX.Domain.Test.Observation.ObservationValueAttachment;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record Data(string Value) : IAttachmentData;
public record Attachment(Data Data) : IObservationValueAttachment;

public record Observation(Status Status, ObservationCode Code, Attachment Value) : IObservation;
