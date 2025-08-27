using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Identifiers;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for the HL7 FHIR Patient contact.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>relationship</term>
///         <description><see cref="IPatientContactRelationship"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>name</term>
///         <description><see cref="IPatientContactHumanName"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>telecom</term>
///         <description><see cref="IPatientContactContactPoint"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address</term>
///         <description><see cref="IPatientContactAddress"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>gender</term>
///         <description><see cref="IPatientContactGender"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>organization</term>
///         <description><see cref="IPatientContactOrganization"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IPatientContactPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("relationship", typeof(IPatientContactRelationship), Cardinality.Multiple)]
[FhirProperty("name", typeof(IPatientContactHumanName), Cardinality.Optional)]
[FhirProperty("telecom", typeof(IPatientContactContactPoint), Cardinality.Multiple)]
[FhirProperty("address", typeof(IPatientContactAddress), Cardinality.Optional)]
[FhirProperty("gender", typeof(IPatientContactGender), Cardinality.Optional)]
[FhirProperty("organization", typeof(IPatientContactOrganization), Cardinality.Optional)]
[FhirProperty("period", typeof(IPatientContactPeriod), Cardinality.Optional)]
public interface IPatientContact : IElement;
