using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for the HL7 FHIR Patient resource.</para>
/// <para>The following elements are supported and may be added as (a colletion of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>identifier</term>
///         <description><see cref="IPatientIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>active</term>
///         <description><see cref="IPatientActive"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>name</term>
///         <description><see cref="IPatientHumanName"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>telecom</term>
///         <description><see cref="IPatientContactPoint"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>gender</term>
///         <description><see cref="IPatientGender"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>birthDate</term>
///         <description><see cref="IPatientBirthDate"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>deceased[Boolean]</term>
///         <description><see cref="IPatientDeceasedBoolean"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>deceased[DateTime]</term>
///         <description><see cref="IPatientDeceasedDateTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address</term>
///         <description><see cref="IPatientAddress"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>maritalStatus</term>
///         <description><see cref="IPatientMaritalStatus"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>multipleBirth[Boolean]</term>
///         <description><see cref="IPatientMultipleBirthBoolean"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>multipleBirth[Integer]</term>
///         <description><see cref="IPatientMultipleBirthInteger"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>photo</term>
///         <description><see cref="IPatientPhoto"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>contact</term>
///         <description><see cref="IPatientContact"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>communication</term>
///         <description><see cref="IPatientCommunication"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>generalPractitioner</term>
///         <description><see cref="IPatientGeneralPractitioner"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>managingOrganization</term>
///         <description><see cref="IPatientManagingOrganization"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>link</term>
///         <description><see cref="IPatientLink"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirResource("Patient")]
[FhirProperty("identifier", typeof(IPatientIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPatientActive), Cardinality.Optional)]
[FhirProperty("name", typeof(IPatientHumanName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IPatientContactPoint), Cardinality.Multiple)]
[FhirProperty("gender", typeof(IPatientGender), Cardinality.Optional)]
[FhirProperty("birthDate", typeof(IPatientBirthDate), Cardinality.Optional)]
[FhirProperty("deceased[Boolean]", typeof(IPatientDeceasedBoolean), Cardinality.Optional)]
[FhirProperty("deceased[DateTime]", typeof(IPatientDeceasedDateTime), Cardinality.Optional)]
[FhirProperty("address", typeof(IPatientAddress), Cardinality.Multiple)]
[FhirProperty("maritalStatus", typeof(IPatientMaritalStatus), Cardinality.Optional)]
[FhirProperty("multipleBirth[Boolean]", typeof(IPatientMultipleBirthBoolean), Cardinality.Optional)]
[FhirProperty("multipleBirth[Integer]", typeof(IPatientMultipleBirthInteger), Cardinality.Optional)]
[FhirProperty("photo", typeof(IPatientPhoto), Cardinality.Multiple)]
[FhirProperty("contact", typeof(IPatientContact), Cardinality.Multiple)]
[FhirProperty("communication", typeof(IPatientCommunication), Cardinality.Multiple)]
[FhirProperty("generalPractitioner", typeof(IPatientGeneralPractitioner), Cardinality.Multiple)]
[FhirProperty("managingOrganization", typeof(IPatientManagingOrganization), Cardinality.Optional)]
[FhirProperty("link", typeof(IPatientLink), Cardinality.Multiple)]
public interface IPatient : IResource;
