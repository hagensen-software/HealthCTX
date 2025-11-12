using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Persons;

namespace HealthCTX.Domain.Test.Persons.PersonMaritalStatus;

public record PersonMaritalStatusCode(string Value) : ICodingCode;
public record PersonMaritalStatusSystem(Uri Value) : ICodingSystem;
public record PersonMaritalStatusDisplay(string Value) : ICodingDisplay;
public record PersonMaritalStatusCoding(
    PersonMaritalStatusCode Code,
    PersonMaritalStatusSystem System,
    PersonMaritalStatusDisplay Display) : ICodeableConceptCoding;

public record PersonMaritalStatusText(string Value) : ICodeableConceptText;

public record PersonMaritalStatus(PersonMaritalStatusCoding Coding, PersonMaritalStatusText Text) : IPersonMaritalStatus;

public record Person(PersonMaritalStatus MaritalStatus) : IPerson;