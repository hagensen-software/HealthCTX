using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Persons;

/// <summary>
/// <para>Interface for the HL7 FHIR Person communication element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>language</term>
///         <description><see cref="IPersonCommunicationLanguage"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>preferred</term>
///         <description><see cref="IPersonCommunicationPreferred"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("language", typeof(IPersonCommunicationLanguage), Cardinality.Mandatory)]
[FhirProperty("preferred", typeof(IPersonCommunicationPreferred), Cardinality.Optional)]
public interface IPersonCommunication : IElement;
