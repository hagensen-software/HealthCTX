using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for the HL7 FHIR Observation component element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>code</term>
///         <description><see cref="IObservationComponentCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Quantity]</term>
///         <description><see cref="IObservationComponentValueQuantity"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[CodeableConcept]</term>
///         <description><see cref="IObservationComponentValueCodeableConcept"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[String]</term>
///         <description><see cref="IObservationComponentValueString"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Boolean]</term>
///         <description><see cref="IObservationComponentValueBoolean"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Integer]</term>
///         <description><see cref="IObservationComponentValueInteger"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Range]</term>
///         <description><see cref="IObservationComponentValueRange"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Ratio]</term>
///         <description><see cref="IObservationComponentValueRatio"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[SampledData]</term>
///         <description><see cref="IObservationComponentValueSampledData"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Time]</term>
///         <description><see cref="IObservationComponentValueTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[DateTime]</term>
///         <description><see cref="IObservationComponentValueDateTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Period]</term>
///         <description><see cref="IObservationComponentValuePeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Attachment]</term>
///         <description><see cref="IObservationComponentValueAttachment"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[Reference]</term>
///         <description><see cref="IObservationComponentValueReference"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>dataAbsentReason</term>
///         <description><see cref="IObservationComponentDataAbsentReason"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>interpretation</term>
///         <description><see cref="IObservationComponentInterpretation"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>referenceRange</term>
///         <description><see cref="IObservationComponentReferenceRange"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("code", typeof(IObservationComponentCode), Cardinality.Mandatory)]
[FhirProperty("value[Quantity]", typeof(IObservationComponentValueQuantity), Cardinality.Optional)]
[FhirProperty("value[CodeableConcept]", typeof(IObservationComponentValueCodeableConcept), Cardinality.Optional)]
[FhirProperty("value[String]", typeof(IObservationComponentValueString), Cardinality.Optional)]
[FhirProperty("value[Boolean]", typeof(IObservationComponentValueBoolean), Cardinality.Optional)]
[FhirProperty("value[Integer]", typeof(IObservationComponentValueInteger), Cardinality.Optional)]
[FhirProperty("value[Range]", typeof(IObservationComponentValueRange), Cardinality.Optional)]
[FhirProperty("value[Ratio]", typeof(IObservationComponentValueRatio), Cardinality.Optional)]
[FhirProperty("value[SampledData]", typeof(IObservationComponentValueSampledData), Cardinality.Optional)]
[FhirProperty("value[Time]", typeof(IObservationComponentValueTime), Cardinality.Optional)]
[FhirProperty("value[DateTime]", typeof(IObservationComponentValueDateTime), Cardinality.Optional)]
[FhirProperty("value[Period]", typeof(IObservationComponentValuePeriod), Cardinality.Optional)]
[FhirProperty("value[Attachment]", typeof(IObservationComponentValueAttachment), Cardinality.Optional)]
[FhirProperty("value[Reference]", typeof(IObservationComponentValueReference), Cardinality.Optional)]
[FhirProperty("dataAbsentReason", typeof(IObservationComponentDataAbsentReason), Cardinality.Optional)]
[FhirProperty("interpretation", typeof(IObservationComponentInterpretation), Cardinality.Multiple)]
[FhirProperty("referenceRange", typeof(IObservationComponentReferenceRange), Cardinality.Multiple)]
public interface IObservationComponent : IElement;
