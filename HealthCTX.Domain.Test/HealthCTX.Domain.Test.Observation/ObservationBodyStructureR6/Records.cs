using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.CodeableReferences;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationBodyStructureR6;

public record Status(string Value) : IObservationStatus;

public record CodeValue(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(CodeValue Code, CodeSystem System) : ICodeableConceptCoding;
public record ObservationCode(ImmutableList<ObservationCodeCoding> Codings) : IObservationCode;

public record BodyStructureConceptCodingCode(string Value) : ICodingCode;
public record BodyStructureConceptCoding(BodyStructureConceptCodingCode Code) : ICodeableConceptCoding;
public record BodyStructureConcept(ImmutableList<BodyStructureConceptCoding> Codings) : ICodeableReferenceConcept;

public record BodyStructureReferenceValue(string Value) : IReferenceReference;
public record BodyStructureReference(BodyStructureReferenceValue Reference) : ICodeableReferenceReference;

public record ObservationBodyStructureR6(
    BodyStructureConcept? Concept,
    BodyStructureReference? Reference) : IObservationBodyStructureR6;

public record Observation(
    Status Status,
    ObservationCode Code,
    ObservationBodyStructureR6? BodyStructure) : IObservation;
