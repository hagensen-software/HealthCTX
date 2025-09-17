using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for the HL7 FHIR Patient link element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>other</term>
///         <description><see cref="IPatientLinkOther"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IPatientLinkType"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("other", typeof(IPatientLinkOther), Cardinality.Mandatory)]
[FhirProperty("type", typeof(IPatientLinkType), Cardinality.Mandatory)]
public interface IPatientLink : IElement;
