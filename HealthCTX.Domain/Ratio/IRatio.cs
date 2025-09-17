using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Ratio;

/// <summary>
/// <para>Interface for the HL7 FHIR Ratio element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>numerator</term>
///         <description><see cref="IRatioNumerator"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>denominator</term>
///         <description><see cref="IRatioDenominator"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("numerator", typeof(IRatioNumerator), Cardinality.Optional)]
[FhirProperty("denominator", typeof(IRatioDenominator), Cardinality.Optional)]
public interface IRatio : IElement;
