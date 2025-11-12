using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonTelecom;

public record PersonTelecomValue(string Value) : IContactPointValue;
public record PersonTelecomSystem(string Value) : IContactPointSystem;
public record PersonTelecom(PersonTelecomSystem System, PersonTelecomValue Value) : IPersonContactPoint;
public record Person(PersonTelecom Telecom) : IPerson;