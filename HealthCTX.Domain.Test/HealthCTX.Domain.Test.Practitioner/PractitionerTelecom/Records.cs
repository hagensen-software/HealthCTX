using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.Practitioner;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerTelecom;

public record PractitionerTelecomValue(string Value) : IContactPointValue;
public record PractitionerTelecomSystem(string Value) : IContactPointSystem;
public record PractitionerTelecom(PractitionerTelecomSystem System, PractitionerTelecomValue Value) : IPractitionerTelecom;
public record Practitioner(PractitionerTelecom Telecom) : IPractitioner;
