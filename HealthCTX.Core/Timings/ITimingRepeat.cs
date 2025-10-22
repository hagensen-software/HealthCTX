using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Timings;

/// <summary>
/// <para>Interface for the HL7 FHIR Timing repeat element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>boundsDuration</term>
///         <description><see cref="ITimingRepeatBoundsDuration"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>boundsRange</term>
///         <description><see cref="ITimingRepeatBoundsRange"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>boundsPeriod</term>
///         <description><see cref="ITimingRepeatBoundsPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>count</term>
///         <description><see cref="ITimingRepeatCount"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>countMax</term>
///         <description><see cref="ITimingRepeatCountMax"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>duration</term>
///         <description><see cref="ITimingRepeatDuration"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>durationMax</term>
///         <description><see cref="ITimingRepeatDurationMax"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>durationUnit</term>
///         <description><see cref="ITimingRepeatDurationUnit"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>frequency</term>
///         <description><see cref="ITimingRepeatFrequency"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>frequencyMax</term>
///         <description><see cref="ITimingRepeatFrequencyMax"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="ITimingRepeatPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>periodMax</term>
///         <description><see cref="ITimingRepeatPeriodMax"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>periodUnit</term>
///         <description><see cref="ITimingRepeatPeriodUnit"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>dayOfWeek</term>
///         <description><see cref="ITimingRepeatDayOfWeek"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>timeOfDay</term>
///         <description><see cref="ITimingRepeatTimeOfDay"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>when</term>
///         <description><see cref="ITimingRepeatWhen"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>offset</term>
///         <description><see cref="ITimingRepeatOffset"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("boundsDuration", typeof(ITimingRepeatBoundsDuration), Cardinality.Optional)]
[FhirProperty("boundsRange", typeof(ITimingRepeatBoundsRange), Cardinality.Optional)]
[FhirProperty("boundsPeriod", typeof(ITimingRepeatBoundsPeriod), Cardinality.Optional)]
[FhirProperty("count", typeof(ITimingRepeatCount), Cardinality.Optional)]
[FhirProperty("countMax", typeof(ITimingRepeatCountMax), Cardinality.Optional)]
[FhirProperty("duration", typeof(ITimingRepeatDuration), Cardinality.Optional)]
[FhirProperty("durationMax", typeof(ITimingRepeatDurationMax), Cardinality.Optional)]
[FhirProperty("durationUnit", typeof(ITimingRepeatDurationUnit), Cardinality.Optional)]
[FhirProperty("frequency", typeof(ITimingRepeatFrequency), Cardinality.Optional)]
[FhirProperty("frequencyMax", typeof(ITimingRepeatFrequencyMax), Cardinality.Optional)]
[FhirProperty("period", typeof(ITimingRepeatPeriod), Cardinality.Optional)]
[FhirProperty("periodMax", typeof(ITimingRepeatPeriodMax), Cardinality.Optional)]
[FhirProperty("periodUnit", typeof(ITimingRepeatPeriodUnit), Cardinality.Optional)]
[FhirProperty("dayOfWeek", typeof(ITimingRepeatDayOfWeek), Cardinality.Multiple)]
[FhirProperty("timeOfDay", typeof(ITimingRepeatTimeOfDay), Cardinality.Multiple)]
[FhirProperty("when", typeof(ITimingRepeatWhen), Cardinality.Multiple)]
[FhirProperty("offset", typeof(ITimingRepeatOffset), Cardinality.Optional)]
public interface ITimingRepeat : IElement;
