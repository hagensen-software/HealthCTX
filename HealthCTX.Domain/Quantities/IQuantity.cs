using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Quantities;

/// <summary>
/// <para>Interface for the HL7 FHIR Quantity element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>value</term>
///         <description><see cref="IQuantityValue"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>comparator</term>
///         <description><see cref="IQuantityComparator"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>unit</term>
///         <description><see cref="IQuantityUnit"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>system</term>
///         <description><see cref="IQuantitySystem"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>code</term>
///         <description><see cref="IQuantityCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("value", typeof(IQuantityValue), Cardinality.Optional)]
[FhirProperty("comparator", typeof(IQuantityComparator), Cardinality.Optional)]
[FhirProperty("unit", typeof(IQuantityUnit), Cardinality.Optional)]
[FhirProperty("system", typeof(IQuantitySystem), Cardinality.Optional)]
[FhirProperty("code", typeof(IQuantityCode), Cardinality.Optional)]
public interface IQuantity : IElement;
