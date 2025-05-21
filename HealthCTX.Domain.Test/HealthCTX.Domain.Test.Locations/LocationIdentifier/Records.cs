using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationIdentifier;

public record IdentifierSystem(Uri Value) : IIdentifierSystem;
public record IdentifierValue(string Value) : IIdentifierValue;
public record Identifier(IdentifierSystem? System, IdentifierValue? Value) : ILocationIdentifier;

public record Location(ImmutableList<Identifier> Identifiers) : ILocation;