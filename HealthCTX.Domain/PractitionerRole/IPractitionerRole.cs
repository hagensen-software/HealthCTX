using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.PractitionerRole;

[FhirResource("PractitionerRole")]
[FhirProperty("identifier", typeof(IPractitionerRoleIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPractitionerRoleActive), Cardinality.Optional)]
[FhirProperty("period", typeof(IPractitionerRolePeriod), Cardinality.Optional)]
[FhirProperty("practitioner", typeof(IPractitionerRolePractitioner), Cardinality.Optional)]
[FhirProperty("organization", typeof(IPractitionerRoleOrganization), Cardinality.Optional)]
[FhirProperty("code", typeof(IPractitionerRoleCode), Cardinality.Multiple)]
[FhirProperty("specialty", typeof(IPractitionerRoleSpecialty), Cardinality.Multiple)]
[FhirProperty("location", typeof(IPractitionerRoleLocation), Cardinality.Multiple)]
[FhirProperty("healthcareService", typeof(IPractitionerRoleHealthcareService), Cardinality.Multiple)]
[FhirProperty("contact", typeof(IPractitionerRoleContact), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("characteristic", typeof(IPractitionerRoleCharacteristic), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("communication", typeof(IPractitionerRoleCommunication), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("availability", typeof(IPractitionerRoleAvailability), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("telecom", typeof(IPractitionerRoleTelecom), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("availableTime", typeof(IPractitionerRoleAvailableTime), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("notAvailable", typeof(IPractitionerRoleNotAvailable), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("availabilityExceptions", typeof(IPractitionerRoleAvailabilityExceptions), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("endpoint", typeof(IPractitionerRoleEndpoint), Cardinality.Multiple)]
public interface IPractitionerRole : IResource;
