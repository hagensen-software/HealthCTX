using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Identifiers;

/// <summary>
/// <para>Interface for the HL7 FHIR Identifier element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>use</term>
///         <description><see cref="IIdentifierUse"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IIdentifierType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>system</term>
///         <description><see cref="IIdentifierSystem"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value</term>
///         <description><see cref="IIdentifierValue"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IIdentifierPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>assigner</term>
///         <description><see cref="IIdentifierAssigner"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("use", typeof(IIdentifierUse), Cardinality.Optional)]
[FhirProperty("type", typeof(IIdentifierType), Cardinality.Optional)]
[FhirProperty("system", typeof(IIdentifierSystem), Cardinality.Optional)]
[FhirProperty("value", typeof(IIdentifierValue), Cardinality.Optional)]
[FhirProperty("period", typeof(IIdentifierPeriod), Cardinality.Optional)]
[FhirProperty("assigner", typeof(IIdentifierAssigner), Cardinality.Optional)]
public interface IIdentifier : IElement;
