using HealthCTX.Domain.PractitionerRole;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleEndpoint;

public record EndpointReference(string Value) : IReferenceReference;
public record PractitionerRoleEndpoint(EndpointReference Reference) : IPractitionerRoleEndpoint;

public record PractitionerRole(PractitionerRoleEndpoint Endpoint) : IPractitionerRole;
