using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonDeceasedDateTime;

public record PersonDeceasedDateTime(DateTimeOffset Value) : IPersonDeceasedDateTime;

public record Person(PersonDeceasedDateTime Deceased) : IPerson;
