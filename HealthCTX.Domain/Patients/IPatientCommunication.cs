using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for the HL7 FHIR Patient communication element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>language</term>
///         <description><see cref="IPatientCommunicationLanguage"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>preferred</term>
///         <description><see cref="IPatientCommunicationPreferred"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("language", typeof(IPatientCommunicationLanguage), Cardinality.Mandatory)]
[FhirProperty("preferred", typeof(IPatientCommunicationPreferred), Cardinality.Optional)]
public interface IPatientCommunication : IElement;
