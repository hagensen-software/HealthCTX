using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observation;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Observation.ObservationBodyStructure;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record BodyStructureReference(string Value) : IReferenceReference;
public record ObservationBodyStructure(BodyStructureReference Reference) : IObservationBodyStructure;

public record Observation(Status Status, ObservationCode Code, ObservationBodyStructure? BodyStructure) : IObservation;
