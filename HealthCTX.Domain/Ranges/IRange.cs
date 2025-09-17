using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Ranges;

/// <summary>
/// <para>Interface for the HL7 FHIR Range element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>low</term>
///         <description><see cref="IRangeLow"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>high</term>
///         <description><see cref="IRangeHigh"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("low", typeof(IRangeLow), Cardinality.Optional)]
[FhirProperty("high", typeof(IRangeHigh), Cardinality.Optional)]
public interface IRange : IElement;
