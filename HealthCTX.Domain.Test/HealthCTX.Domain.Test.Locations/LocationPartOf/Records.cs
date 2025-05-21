using HealthCTX.Domain.Locations;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Locations.LocationPartOf;

public record LocationReference(string Value) : IReferenceReference;
public record LocationPartOf(LocationReference? Reference) : ILocationPartOf;

public record Location(LocationPartOf? PartOf) : ILocation;