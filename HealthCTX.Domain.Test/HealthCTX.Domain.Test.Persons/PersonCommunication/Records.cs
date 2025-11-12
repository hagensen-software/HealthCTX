using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Persons;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Persons.PersonCommunication;

public record PersonLanguageCode(string Value) : ICodingCode;
public record PersonLanguageSystem(Uri Value) : ICodingSystem;
public record PersonLanguageDisplay(string Value) : ICodingDisplay;
public record PersonLanguageCoding(PersonLanguageCode Code, PersonLanguageSystem System, PersonLanguageDisplay Display) : ICodeableConceptCoding;
public record PersonCommunicationLanguage(PersonLanguageCoding Coding) : IPersonCommunicationLanguage;

public record PersonPreferred(bool Value) : IPersonCommunicationPreferred;

public record PersonCommunication(PersonCommunicationLanguage Language, PersonPreferred Preferred) : IPersonCommunication;

public record Person(ImmutableList<PersonCommunication> Communication) : IPerson;
