using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Observation.ObservationInstantiatesReference;

public record Status(string Value) : IObservationStatus;

public record Code(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(
    Code Code,
    CodeSystem System) : ICodeableConceptCoding;

public record ObservationCode(ObservationCodeCoding Coding) : IObservationCode;

public record InstantiatesReference(string Value) : IReferenceReference;
public record Instantiates(InstantiatesReference Reference) : IObservationInstantiatesReference;

public record Observation(Status Status, ObservationCode Code, Instantiates? Instantiates) : IObservation;
