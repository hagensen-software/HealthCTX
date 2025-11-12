using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonBirthDate;

public record PersonBirthDate(DateOnly Value) : IPersonBirthDate;

public record Person(PersonBirthDate BirthDate) : IPerson;
