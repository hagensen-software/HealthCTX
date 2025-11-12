using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Persons;

/// <summary>
/// <para>Interface for the HL7 FHIR Person link element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>target</term>
///         <description><see cref="IPersonLinkTarget"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>assurance</term>
///         <description><see cref="IPersonLinkAssurance"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("target", typeof(IPersonLinkTarget), Cardinality.Mandatory)]
[FhirProperty("assurance", typeof(IPersonLinkAssurance), Cardinality.Optional)]
public interface IPersonLink : IElement;
