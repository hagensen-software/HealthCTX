using HealthCTX.Domain.Locations;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Locations.LocationManagingOrganization;

public record OrganizationReference(string Value) : IReferenceReference;
public record ManagingOrganization(OrganizationReference? Reference) : ILocationManagingOrganization;

public record Location(ManagingOrganization? ManagingOrganization) : ILocation;
