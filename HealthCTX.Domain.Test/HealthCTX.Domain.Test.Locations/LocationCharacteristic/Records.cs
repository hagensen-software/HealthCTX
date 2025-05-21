using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationCharacteristic;

public record CharacteristicSystem(Uri Value) : ICodingSystem;
public record CharacteristicCode(string Value) : ICodingCode;
public record CharacteristicCoding(CharacteristicSystem? System, CharacteristicCode? Code) : ICodeableConceptCoding;
public record LocationCharacteristic(ImmutableList<CharacteristicCoding> Codings) : ILocationCharacteristic;
public record Location(ImmutableList<LocationCharacteristic> Types) : ILocation;
