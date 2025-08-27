using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterServiceTypeR4;

public record Status(string Value) : IEncounterStatus;

public record ServiceTypeSystem(Uri Value) : ICodingSystem;
public record ServiceTypeCode(string Value) : ICodingCode;
public record ServiceTypeCoding(ServiceTypeSystem? System, ServiceTypeCode? Code) : ICodeableConceptCoding;

public record EncounterServiceType(ImmutableList<ServiceTypeCoding> Codings) : IEncounterServiceTypeConcept;

public record Encounter(Status Status, EncounterServiceType? ServiceType) : IEncounter;
