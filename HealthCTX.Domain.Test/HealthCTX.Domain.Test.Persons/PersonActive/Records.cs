using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonActive;

public record PersonActive(bool Value) : IPersonActive;
public record Person(PersonActive Active) : IPerson;