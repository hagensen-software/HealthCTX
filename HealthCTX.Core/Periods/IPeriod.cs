using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Periods;

/// <summary>
/// <para>Interface for the HL7 FHIR Identifier element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>start</term>
///         <description><see cref="IPeriodStart"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>end</term>
///         <description><see cref="IPeriodEnd"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("start", typeof(IPeriodStart), Cardinality.Optional)]
[FhirProperty("end", typeof(IPeriodEnd), Cardinality.Optional)]
public interface IPeriod : IElement;
