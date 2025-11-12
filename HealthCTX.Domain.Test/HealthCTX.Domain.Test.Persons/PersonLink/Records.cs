using HealthCTX.Domain.Persons;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Persons.PersonLink;

public record PersonLinkOtherReference(string Value) : IReferenceReference;
public record PersonLinkTarget(PersonLinkOtherReference Reference) : IPersonLinkTarget;

public record PersonLinkAssurance(string Value) : IPersonLinkAssurance;

public record PersonLink(PersonLinkTarget Target, PersonLinkAssurance Assurance) : IPersonLink;

public record Person(PersonLink Link) : IPerson;
