using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.PractitionerRole;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleTelecom;

public record PractitionerRoleTelecomValue(string Value) : IContactPointValue;
public record PractitionerRoleTelecomSystem(string Value) : IContactPointSystem;
public record PractitionerRoleTelecom(PractitionerRoleTelecomSystem System, PractitionerRoleTelecomValue Value) : IPractitionerRoleTelecom;
public record PractitionerRole(PractitionerRoleTelecom Telecom) : IPractitionerRole;
