using HealthCTX.Domain.Locations;
using HealthCTX.Domain.References;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationEndpoint;

public record EndpointReference(string Value) : IReferenceReference;
public record Endpoint(EndpointReference? Reference) : ILocationEndpoint;

public record Location(ImmutableList<Endpoint> Endpoints) : ILocation;

