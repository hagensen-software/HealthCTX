using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.PractitionerRole;

/// <summary>
/// <para>Interface for the HL7 FHIR PractitionerRole resource.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>identifier</term>
///         <description><see cref="IPractitionerRoleIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>active</term>
///         <description><see cref="IPractitionerRoleActive"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IPractitionerRolePeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>practitioner</term>
///         <description><see cref="IPractitionerRolePractitioner"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>organization</term>
///         <description><see cref="IPractitionerRoleOrganization"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>code</term>
///         <description><see cref="IPractitionerRoleCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>specialty</term>
///         <description><see cref="IPractitionerRoleSpecialty"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>location</term>
///         <description><see cref="IPractitionerRoleLocation"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>healthcareService</term>
///         <description><see cref="IPractitionerRoleHealthcareService"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>contact</term>
///         <description><see cref="IPractitionerRoleContact"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>characteristic</term>
///         <description><see cref="IPractitionerRoleCharacteristic"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>communication</term>
///         <description><see cref="IPractitionerRoleCommunication"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>availability</term>
///         <description><see cref="IPractitionerRoleAvailability"/> (HL7 FHIR R5 only)</description>
///     </item>
///     <item>
///         <term>telecom</term>
///         <description><see cref="IPractitionerRoleTelecom"/> (HL7 FHIR R4 only)</description>
///     </item>
///     <item>
///         <term>availableTime</term>
///         <description><see cref="IPractitionerRoleAvailableTime"/> (HL7 FHIR R4 only)</description>
///     </item>
///     <item>
///         <term>notAvailable</term>
///         <description><see cref="IPractitionerRoleNotAvailable"/> (HL7 FHIR R4 only)</description>
///     </item>
///     <item>
///         <term>availabilityExceptions</term>
///         <description><see cref="IPractitionerRoleAvailabilityExceptions"/> (HL7 FHIR R4 only)</description>
///     </item>
///     <item>
///         <term>endpoint</term>
///         <description><see cref="IPractitionerRoleEndpoint"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
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
