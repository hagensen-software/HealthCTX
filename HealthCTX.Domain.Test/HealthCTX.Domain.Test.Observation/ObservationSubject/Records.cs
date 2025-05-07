using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Observation.ObservationSubject;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record SubjectReference(string Value) : IReferenceReference;
public record Subject(SubjectReference Reference) : IObservationSubject;

public record Observation(Status Status, ObservationCode Code, Subject? Subject) : IObservation;
