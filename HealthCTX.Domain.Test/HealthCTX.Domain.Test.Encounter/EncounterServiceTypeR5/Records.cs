using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.CodeableReferences;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterServiceTypeR5;

public record Status(string Value) : IEncounterStatus;

public record ServiceTypeSystem(Uri Value) : ICodingSystem;
public record ServiceTypeCode(string Value) : ICodingCode;
public record ServiceTypeCoding(ServiceTypeSystem? System, ServiceTypeCode? Code) : ICodeableConceptCoding;
public record ServiceTypeConcept(ImmutableList<ServiceTypeCoding> Codings) : ICodeableReferenceConcept;
public record EncounterServiceType(ServiceTypeConcept? Concept) : IEncounterServiceType;

public record Encounter(Status Status, ImmutableList<EncounterServiceType> ServiceTypes) : IEncounter;
