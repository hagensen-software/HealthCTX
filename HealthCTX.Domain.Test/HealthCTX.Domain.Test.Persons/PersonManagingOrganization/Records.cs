using HealthCTX.Domain.Persons;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.Persons.PersonManagingOrganization;

public record PersonManagingOrganizationReference(string Value) : IReferenceReference;
public record PersonManagingOrganization(PersonManagingOrganizationReference Reference) : IPersonManagingOrganization;

public record Person(PersonManagingOrganization ManagingOrganization) : IPerson;