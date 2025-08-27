using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterDietPreference;

public record Status(string Value) : IEncounterStatus;

public record DietPreferenceSystem(Uri Value) : ICodingSystem;
public record DietPreferenceCode(string Value) : ICodingCode;
public record DietPreferenceCoding(DietPreferenceSystem? System, DietPreferenceCode? Code) : ICodeableConceptCoding;

public record EncounterDietPreference(ImmutableList<DietPreferenceCoding> Codings) : IEncounterDietPreference;

public record Encounter(Status Status, ImmutableList<EncounterDietPreference> DietPreferences) : IEncounter;
