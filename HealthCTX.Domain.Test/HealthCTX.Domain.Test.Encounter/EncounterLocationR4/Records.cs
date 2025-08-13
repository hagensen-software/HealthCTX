using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Period;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterLocationR4;

public record Status(string Value) : IEncounterStatus;

public record LocationReference(string Value) : IReferenceReference;
public record LocationLocation(LocationReference? Reference) : IEncounterLocationLocation;

public record LocationStatus(string Value) : IEncounterLocationStatus;

public record PhysicalTypeSystem(Uri Value) : ICodingSystem;
public record PhysicalTypeCode(string Value) : ICodingCode;
public record PhysicalTypeCoding(PhysicalTypeSystem? System, PhysicalTypeCode? Code) : ICodeableConceptCoding;

public record LocationPhysicalType(ImmutableList<PhysicalTypeCoding> Codings) : IEncounterLocationPhysicalType;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record LocationPeriod(PeriodStart Start, PeriodEnd End) : IEncounterLocationPeriod;

public record EncounterLocation(LocationLocation Location, LocationStatus? Status, LocationPhysicalType? PhysicalType, LocationPeriod? Period) : IEncounterLocation;

public record Encounter(Status Status, ImmutableList<EncounterLocation> Locations) : IEncounter;
