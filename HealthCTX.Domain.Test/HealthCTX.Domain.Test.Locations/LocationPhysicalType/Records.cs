using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationPhysicalType;

public record PhysicalTypeSystem(Uri Value) : ICodingSystem;
public record PhysicalTypeCode(string Value) : ICodingCode;
public record PhysicalTypeCoding(PhysicalTypeSystem? System, PhysicalTypeCode? Code) : ICodeableConceptCoding;
public record PhysicalType(ImmutableList<PhysicalTypeCoding> Codings) : ILocationPhysicalType;

public record Location(PhysicalType? PhysicalType) : ILocation;