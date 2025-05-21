using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationType;

public record TypeSystem(Uri Value) : ICodingSystem;
public record TypeCode(string Value) : ICodingCode;
public record TypeCoding(TypeSystem? System, TypeCode? Code) : ICodeableConceptCoding;
public record LocationType(ImmutableList<TypeCoding> Codings) : ILocationType;
public record Location(ImmutableList<LocationType> Types) : ILocation; 
