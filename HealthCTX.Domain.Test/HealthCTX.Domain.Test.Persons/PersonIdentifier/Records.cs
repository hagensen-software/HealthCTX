using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonIdentifier;

public record PersonIdentifierSystem(Uri Value) : IIdentifierSystem;
public record PersonIdentifierValue(string Value) : IIdentifierValue;
public record PersonIdentifier(PersonIdentifierSystem System, PersonIdentifierValue Value) : IPersonIdentifier;

public record Person(PersonIdentifier Identifier) : IPerson;