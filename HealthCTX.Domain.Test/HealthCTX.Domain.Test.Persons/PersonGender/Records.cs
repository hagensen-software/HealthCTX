using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonGender;

public record PersonGender(string Value) : IPersonGender;

public record Person(PersonGender Gender) : IPerson;