using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.Quantities;

namespace HealthCTX.Domain.Test.Encounter.EncounterLength;

public record Status(string Value) : IEncounterStatus;

public record QuantityValue(double Value) : IQuantityValue;
public record QuantityUnit(string Value) : IQuantityUnit;
public record EncounterLength(QuantityValue Value, QuantityUnit Unit) : IEncounterLength;

public record Encounter(Status Status, EncounterLength? Length) : IEncounter;
