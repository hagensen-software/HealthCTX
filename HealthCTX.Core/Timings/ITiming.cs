using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Timings;

/// <summary>
/// <para>Interface for the HL7 FHIR Timing element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>event</term>
///         <description><see cref="ITimingEvent"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>repeat</term>
///         <description><see cref="ITimingRepeat"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>code</term>
///         <description><see cref="ITimingCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("event", typeof(ITimingEvent), Cardinality.Multiple)]
[FhirProperty("repeat", typeof(ITimingRepeat), Cardinality.Optional)]
[FhirProperty("code", typeof(ITimingCode), Cardinality.Optional)]
public interface ITiming : IElement;
