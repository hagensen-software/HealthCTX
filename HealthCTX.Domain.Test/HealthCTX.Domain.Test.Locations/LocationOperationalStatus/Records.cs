using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Locations;

namespace HealthCTX.Domain.Test.Locations.LocationOperationalStatus;

public record OperationalStatusSystem(Uri Value) : ICodingSystem;
public record OperationalStatusCode(string Value) : ICodingCode;
public record OperationalStatus(OperationalStatusSystem? System, OperationalStatusCode? Code) : ILocationOperationalStatus;

public record Location(OperationalStatus? OperationalStatus) : ILocation;