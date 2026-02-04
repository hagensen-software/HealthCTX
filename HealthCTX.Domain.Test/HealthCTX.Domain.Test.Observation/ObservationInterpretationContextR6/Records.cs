using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.CodeableReferences;
using HealthCTX.Domain.Observations;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Observation.ObservationInterpretationContextR6;

public record Status(string Value) : IObservationStatus;

public record CodeValue(string Value) : ICodingCode;
public record CodeSystem(Uri Value) : ICodingSystem;
public record ObservationCodeCoding(CodeValue Code, CodeSystem System) : ICodeableConceptCoding;
public record ObservationCode(ImmutableList<ObservationCodeCoding> Codings) : IObservationCode;

public record InterpretationContextConceptCodingCode(string Value) : ICodingCode;
public record InterpretationContextConceptCoding(InterpretationContextConceptCodingCode Code) : ICodeableConceptCoding;
public record InterpretationContextConcept(ImmutableList<InterpretationContextConceptCoding> Codings) : ICodeableReferenceConcept;

public record InterpretationContextReferenceValue(string Value) : IReferenceReference;
public record InterpretationContextReference(InterpretationContextReferenceValue Reference) : ICodeableReferenceReference;

public record ObservationInterpretationContext(
    InterpretationContextConcept? Concept,
    InterpretationContextReference? Reference) : IObservationInterpretationContext;

public record Observation(
    Status Status,
    ObservationCode Code,
    ImmutableList<ObservationInterpretationContext> InterpretationContexts) : IObservation;
