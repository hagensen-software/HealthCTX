using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Periods;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterLocationR5;

public record Status(string Value) : IEncounterStatus;

public record LocationReference(string Value) : IReferenceReference;
public record LocationLocation(LocationReference? Reference) : IEncounterLocationLocation;

public record LocationStatus(string Value) : IEncounterLocationStatus;

public record FormSystem(Uri Value) : ICodingSystem;
public record FormCode(string Value) : ICodingCode;
public record FormCoding(FormSystem? System, FormCode? Code) : ICodeableConceptCoding;

public record LocationForm(ImmutableList<FormCoding> Codings) : IEncounterLocationForm;

public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record LocationPeriod(PeriodStart Start, PeriodEnd End) : IEncounterLocationPeriod;

public record EncounterLocation(LocationLocation Location, LocationStatus? Status, LocationForm? Form, LocationPeriod? Period) : IEncounterLocation;

public record Encounter(Status Status, ImmutableList<EncounterLocation> Locations) : IEncounter;
