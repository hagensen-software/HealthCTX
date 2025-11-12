using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Persons;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Persons.PersonName;

public record PersonFamilyName(string Value) : IHumanNameFamily;
public record PersonGivenName(string Value) : IHumanNameGiven;

public record PersonName(PersonFamilyName Family, ImmutableList<PersonGivenName> Given) : IPersonHumanName;

public record Person(PersonName Name) : IPerson;