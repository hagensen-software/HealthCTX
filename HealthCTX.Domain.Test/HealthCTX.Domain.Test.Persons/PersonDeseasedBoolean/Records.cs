using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonDeseasedBoolean;

public record PersonDeceasedBoolean(bool Value) : IPersonDeceasedBoolean;

public record Person(PersonDeceasedBoolean Deceased) : IPerson;
