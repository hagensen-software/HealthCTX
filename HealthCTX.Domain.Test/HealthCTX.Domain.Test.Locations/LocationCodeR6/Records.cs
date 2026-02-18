using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationCodeR6;

public record LocationCodeCodingCode(string Value) : ICodingCode;
public record LocationCodeCoding(LocationCodeCodingCode Code) : ICodeableConceptCoding;
public record LocationCode(ImmutableList<LocationCodeCoding> Codings) : ILocationCode;

public record Location(LocationCode? Code) : ILocation;
