using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Persons;

/// <summary>
/// <para>Interface for the HL7 FHIR Person resource.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table"> 
///     <item>
///         <term>identifier</term>
///         <description><see cref="IPersonIdentifier"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>active</term>
///         <description><see cref="IPersonActive"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>name</term>
///         <description><see cref="IPersonHumanName"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>telecom</term>
///         <description><see cref="IPersonContactPoint"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>gender</term>
///         <description><see cref="IPersonGender"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>birthDate</term>
///         <description><see cref="IPersonBirthDate"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>deceased[Boolean]</term>
///         <description><see cref="IPersonDeceasedBoolean"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>deceased[DateTime]</term>
///         <description><see cref="IPersonDeceasedDateTime"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>address</term>
///         <description><see cref="IPersonAddress"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>maritalStatus</term>
///         <description><see cref="IPersonMaritalStatus"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>photo</term>
///         <description><see cref="IPersonPhotoR4"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>photo</term>
///         <description><see cref="IPersonPhoto"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>communication</term>
///         <description><see cref="IPersonCommunication"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>managingOrganization</term>
///         <description><see cref="IPersonManagingOrganization"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>link</term>
///         <description><see cref="IPersonLink"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirResource("Person")]
[FhirProperty("identifier", typeof(IPersonIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPersonActive), Cardinality.Optional)]
[FhirProperty("name", typeof(IPersonHumanName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IPersonContactPoint), Cardinality.Multiple)]
[FhirProperty("gender", typeof(IPersonGender), Cardinality.Optional)]
[FhirProperty("birthDate", typeof(IPersonBirthDate), Cardinality.Optional)]
[FhirProperty("deceased[Boolean]", typeof(IPersonDeceasedBoolean), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("deceased[DateTime]", typeof(IPersonDeceasedDateTime), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("address", typeof(IPersonAddress), Cardinality.Multiple)]
[FhirProperty("maritalStatus", typeof(IPersonMaritalStatus), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("photo", typeof(IPersonPhotoR4), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("photo", typeof(IPersonPhoto), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("communication", typeof(IPersonCommunication), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("managingOrganization", typeof(IPersonManagingOrganization), Cardinality.Optional)]
[FhirProperty("link", typeof(IPersonLink), Cardinality.Multiple)]
public interface IPerson : IResource;
